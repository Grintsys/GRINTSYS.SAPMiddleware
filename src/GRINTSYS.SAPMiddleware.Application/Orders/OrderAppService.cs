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
using GRINTSYS.SAPMiddleware.M2.Clients;
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
        private readonly ClientManager _clientManager;
        //TODO: please find a better solution insted of create a user repository
        private readonly IRepository<User, long> _userRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAbpSession _session;

        public OrderAppService(OrderManager orderManager, 
            CartManager cartManager,
            ProductManager productManager,
            ClientManager clientManager,
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
            _clientManager = clientManager;
        }

        public async Task CreateOrder(CreateOrderInput input)
        {
            
            var orderId = new int[2];
            var userId = GetUserId();

            var tenant = await GetCurrentTenantAsync();

            if (!input.TenantId.HasValue && String.IsNullOrEmpty(input.CardCode))
                throw new UserFriendlyException("Tenant or CardCode is not present");

            //FIX: i don't want to do this here but i dont have to think a better solution, the problem is that this funtion is overheading this function 
            var client = _clientManager.GetClientByCardCode(input.CardCode, input.TenantId.Value);

            var newOrder = new Order()
            {
                TenantId = input.TenantId.Value,
                UserId = userId,
                Status = OrderStatus.CreadoEnAplicacion,
                DeliveryDate = input.DeliveryDate,
                Comment = input.Comment,
                CardCode = input.CardCode,
                CardName = client.Name
            };

            orderId[0] = await _orderManager.CreateOrder(newOrder);

            if (tenant.TenancyName.Equals("Guatemala"))
            {
                var newOrder2 = new Order()
                {
                    TenantId = 2, //Honduras
                    UserId = 7, //GTK.M2
                    Status = OrderStatus.CreadoEnAplicacion,
                    DeliveryDate = input.DeliveryDate,
                    Comment = input.Comment,
                    CardCode = "C1150",
                    CardName = "DISTRIBUIDORA DE VESTUARIO GTK S. A."
                };

                orderId[1] = await _orderManager.CreateOrder2(newOrder2);
            }

            var products = _cartManager.GetCartProductItemsByUser(userId, input.TenantId.Value);

            foreach (var item in products.OrderBy(p=>p.Variant.Code))
            {
                var newOrderItem = new OrderItem()
                {
                    TenantId = input.TenantId.Value,
                    OrderId = orderId[0],
                    Code = item.Variant.Code,
                    Name = string.Empty,
                    Quantity = item.Quantity,
                    Price = item.Variant.Price,
                    Discount = 0,
                    DiscountPercent = 0,
                    TaxValue = tenant.ISV,
                    TaxCode = string.Empty, //falta esto
                    WarehouseCode = item.Variant.WareHouseCode
                };

                await _orderManager.CreateOrderItem(newOrderItem);
                //actualiza el comprometido pero solo en M2
                await _productManager.UpdateProductStock(item.Variant.Id, item.Quantity);

                if (tenant.TenancyName.Equals("Guatemala"))
                {
                    var newOrderItem2 = new OrderItem()
                    {
                        TenantId = 2,
                        OrderId = orderId[1],
                        Code = item.Variant.Code,
                        Name = string.Empty,
                        Quantity = item.Quantity,
                        Price = 1,
                        Discount = 0,
                        DiscountPercent = 0,
                        TaxValue = 0,
                        TaxCode = string.Empty, //falta esto
                        WarehouseCode = item.Variant.WareHouseCode
                    };

                    await _orderManager.CreateOrderItem(newOrderItem2);
                }
            }

            //Delete the user cart
            await _cartManager.DeleteUserCart(userId, input.TenantId.Value);

            //Hey this send to SAP using hangfire backgroundjobs
            string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], orderId[0]);
            await AppConsts.Instance.GetClient().GetAsync(url);

            if (tenant.TenancyName.Equals("Guatemala"))
            {
                string url2 = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], orderId[1]);
                await AppConsts.Instance.GetClient().GetAsync(url2);
            }
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

            var orderOutput = new OrderOutput()
            {
                Id = order.Id,
                RemoteId = order.RemoteId,
                CreationTime = order.CreationTime.ToString("dd/MMM/yyyy"),
                Status = ((OrderStatus)order.Status).ToString(),
                CardCode = order.CardCode + "|" + order.CardName,
                Comment = order.Comment,
                DeliveryDate = order.DeliveryDate,
                Series = order.Series,
                SubTotal = order.GetSubtotal(),
                Discount = order.GetDiscount(),
                IVA = order.GetIVA(),
                Total = order.GetTotal(),
                Items = order.OrderItems.Select(s=> new OrderItemOutput()
                {
                   Code = s.Code,
                   Name = s.Name,
                   Quantity = s.Quantity,
                   Price = (s.Quantity * s.Price),
                   Discount = (s.Discount),
                   DiscountPercent = s.DiscountPercent,
                   TaxValue = s.TaxValue,
                   TaxCode = s.TaxCode,
                   WarehouseCode = s.WarehouseCode
                }).OrderBy(o=>o.Code)
                  .ToList()
            };

            return orderOutput;
        }

        public PagedResultDto<OrderOutput> GetOrders(GetAllOrderInput input)
        {
            if (_session.TenantId.HasValue)
                input.TenantId = (int)_session.TenantId;
            else
                return new PagedResultDto<OrderOutput>();

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
                    CardCode = s.CardCode + "|" + s.CardName,
                    Comment = s.Comment,
                    LastMessage = s.LastMessage,
                    Status = ((OrderStatus)s.Status).ToString(),
                    CreationTime = s.CreationTime.ToString("dd/MMM/yyyy"),
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
                    Id = s.Id,
                    CardCode = s.CardCode + "|" + s.CardName,
                    Comment = s.Comment,
                    LastMessage = s.LastMessage,
                    Status = ((OrderStatus)s.Status).ToString(),
                    CreationTime = s.CreationTime.ToString("dd/MMM/yyyy"),
                    Total = s.GetTotal(),
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
