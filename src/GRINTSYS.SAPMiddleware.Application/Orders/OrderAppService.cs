using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using GRINTSYS.SAPMiddleware.Orders.Job;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders
{
    public class OrderAppService : SAPMiddlewareAppServiceBase, IOrderAppService
    {
        private readonly OrderManager _orderManager;
        private readonly CartManager _cartManager;
        private readonly ProductManager _productManager;
        //TODO: please find a better solution insted of create a user repository
        private readonly IRepository<User, long> _userRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAbpSession _session;

        public OrderAppService(OrderManager orderManager, 
            CartManager cartManager,
            ProductManager productManager,
            IRepository<User, long> userRepository,
            IBackgroundJobManager backgroundJobManager,
            IAbpSession session)
        {
            _orderManager = orderManager;
            _cartManager = cartManager;
            _productManager = productManager;
            _userRepository = userRepository;
            _backgroundJobManager = backgroundJobManager;
            _session = session;
        }

        public async Task CreateOrder(CreateOrderInput input)
        {
            var userId = GetUserId();

            var tenant = await GetCurrentTenantAsync();

            if (!input.TenantId.HasValue && String.IsNullOrEmpty(input.CardCode))
                throw new UserFriendlyException("Tenant or CardCode is not present");

            //we don't need to change the order default database (Honduras is the default)
            if (tenant.TenancyName.Equals("Guatemala")){
                input.CardCode = "C1150";
                input.Comment = "GT|" + input.Comment;
            }

            var newOrder = new Order()
            {
                TenantId = input.TenantId.Value,
                UserId = userId,
                Status = OrderStatus.CreadoEnAplicacion,
                DeliveryDate = input.DeliveryDate,
                Comment = input.Comment,
                CardCode = input.CardCode
            };

            var orderId = await _orderManager.CreateOrder(newOrder);

            var products = _cartManager.GetCartProductItemsByUser(userId, input.TenantId.Value);

            foreach (var item in products)
            {
                var newOrderItem = new OrderItem()
                {
                    TenantId = input.TenantId.Value,
                    OrderId = orderId,
                    Code = item.Variant.Code,
                    Quantity = item.Quantity,
                    Price = item.Variant.Price,
                    TaxCode = "", //falta esto
                    WarehouseCode = item.Variant.WareHouseCode
                };

                await _orderManager.CreateOrderItem(newOrderItem);
                //actualiza el comprometido pero solo en M2
                await _productManager.UpdateProductStock(item.Variant.Id, item.Quantity);
            }

            //Delete the user cart
            await _cartManager.DeleteUserCart(userId, input.TenantId.Value);

            //Hey this send to SAP using hangfire backgroundjobs
            string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], orderId);
            await AppConsts.Instance.GetClient().GetAsync(url);
        }

        public async Task DeleteOrder(DeleteOrderInput input)
        {
            var order = _orderManager.GetOrder(input.OrderId);

            if (order == null)
                throw new UserFriendlyException("Order is not found");

            await _orderManager.DeleteOrderAsync(order);
        }

        public OrderOutput GetOrder(GetOrderInput input)
        {
            var order = _orderManager.GetOrder(input.Id);

            return new OrderOutput()
            {
                Id = order.Id,
                RemoteId = order.RemoteId,
                CardCode = order.CardCode,
                Status = ((OrderStatus)order.Status).ToString(),
                Comment = order.Comment,
                CreationTime = order.CreationTime,
                DeliveryDate = order.DeliveryDate,
                Series = order.Series//,
                //Items = order.OrderItems.MapTo<List<OrderItemOutput>>()
            };
        }

        public PagedResultDto<OrderOutput> GetOrders(GetAllOrderInput input)
        {
            if (input.TenantId == 0)
                input.TenantId = (int)_session.TenantId;

            if (String.IsNullOrEmpty(input.begin))
                input.begin = DateTime.MinValue.ToString();

            if (String.IsNullOrEmpty(input.end))
                input.end = DateTime.MaxValue.ToString();

            var orders = _orderManager.GetOrders(input.TenantId,
                DateTime.Parse(input.begin),
                DateTime.Parse(input.end))
                .Select(s => new OrderOutput()
                {
                    Id = s.Id,
                    CardCode = s.CardCode,
                    Comment = s.Comment,
                    LastMessage = s.LastMessage,
                    Status = ((OrderStatus)s.Status).ToString(),
                    Total = s.GetTotal(),
                    TotalFormatted = s.GetTotal().ToString()
                })
                .OrderByDescending(o => o.Id)
                .ToList();

            var total = orders.Count();

            return new PagedResultDto<OrderOutput>()
            {
                TotalCount = total,
                Items = orders
            };
        }


        //this methods need some refactor
        public PagedResultDto<OrderOutput> GetOrdersByUser(GetAllOrderInput input)
        {
            var userId = GetUserId();

            if (String.IsNullOrEmpty(input.begin))
                input.begin = DateTime.MinValue.ToString();

            if (String.IsNullOrEmpty(input.end))
                input.end = DateTime.MaxValue.ToString();

            var orders = _orderManager.GetOrdersByUser(input.TenantId,
                userId,
                DateTime.Parse(input.begin),
                DateTime.Parse(input.end))
                .Select(s => new OrderOutput()
                {
                    CardCode = s.CardCode,
                    Comment = s.Comment,
                    LastMessage = s.LastMessage,
                    Status = ((OrderStatus)s.Status).ToString(),
                    TotalFormatted = s.GetTotal().ToString()
                })
                .ToList();

            var total = orders.Count();

            return new PagedResultDto<OrderOutput>()
            {
                TotalCount = total,
                Items = orders
            };
        }

        public List<SellerOutput> GetSellers(GetAllSellerInput input)
        {
            var users = _userRepository.GetAll()
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                ;

            return users.MapTo<List<SellerOutput>>();
        }

        public async Task GetOrderInSap(SentToSapInput input)
        {
            await _backgroundJobManager.EnqueueAsync<OrderToSAPJob, int>(input.Id);
            /*
            Logger.Debug(String.Format("SendToSap({0})", input.Id));
            string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], input.Id);
            var response = await AppConsts.Instance.GetClient().GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Logger.Info("Success to send to SAP");
            }*/
        }
    }
}
