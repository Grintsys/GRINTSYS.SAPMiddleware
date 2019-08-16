using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper.QueryableExtensions;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using GRINTSYS.SAPMiddleware.Orders.Job;

namespace GRINTSYS.SAPMiddleware.Orders
{
    public class OrderAppService : SAPMiddlewareAppServiceBase, IOrderAppService
    {
        private readonly OrderManager _orderManager;
        private readonly CartManager _cartManager;
        //TODO: please find a better solution insted of create a user repository
        private readonly IRepository<User, long> _userRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public OrderAppService(OrderManager orderManager, 
            CartManager cartManager,
            IRepository<User, long> userRepository,
            IBackgroundJobManager backgroundJobManager)
        {
            _orderManager = orderManager;
            _cartManager = cartManager;
            _userRepository = userRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task CreateOrder(AddOrderInput input)
        {
            var userId = GetUserId();

            if (!input.TenantId.HasValue && String.IsNullOrEmpty(input.CardCode))
                throw new UserFriendlyException("Tenant or CardCode is not present");

            await _backgroundJobManager.EnqueueAsync<OrderJob, OrderParams>(
                new OrderParams
                {
                     TenantId = input.TenantId.Value,
                     UserId = userId,
                     CardCode = input.CardCode,
                     Comment = input.Comment,
                     DeliveryDate = input.DeliveryDate
                });
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
            var userId = GetUserId();

            var orders = _orderManager.GetOrders(input.TenantId, 
                userId, 
                DateTime.Parse(input.begin), DateTime.Parse(input.end));

            return new PagedResultDto<OrderOutput>()
            {
                Items = orders.MapTo<List<OrderOutput>>()
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
            Logger.Debug(String.Format("SendToSap({0})", input.Id));
            string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], input.Id);
            var response = await AppConsts.Instance.GetClient().GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Logger.Info("Success to send to SAP");
            }
        }
    }
}
