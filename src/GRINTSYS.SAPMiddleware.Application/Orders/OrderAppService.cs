using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.Orders.Dto;

namespace GRINTSYS.SAPMiddleware.Orders
{
    public class OrderAppService : SAPMiddlewareAppServiceBase, IOrderAppService
    {
        private readonly OrderManager _orderManager;
        private readonly CartManager _cartManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public OrderAppService(OrderManager orderManager, 
            CartManager cartManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _orderManager = orderManager;
            _cartManager = cartManager;
            _backgroundJobManager = backgroundJobManager;
        }

        public long GetUserId()
        {
            var userId = long.MinValue;
            try
            {
                userId = AbpSession.GetUserId();
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Expired Session");
            }

            return userId;
        }

        public async Task CreateOrder(AddOrderInput input)
        {
            var userId = GetUserId();

            var newOrder = new Order()
            {
                TenantId = input.TenantId,
                UserId = userId,
                Status = OrderStatus.CreadoEnAplicacion,
                DeliveryDate = input.DeliveryDate,
                Comment = input.Comment,
                CardCode = input.CardCode
            };

            var order = await _orderManager.CreateOrder(newOrder);

            var cart = _cartManager.GetCartProductItemsByUser(userId, input.TenantId);

            //_cartManager.GetCartProductItems()
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
                Series = order.Series,
                Items = order.OrderItems.MapTo<List<OrderItemOutput>>()
            };
        }

        public List<OrderOutput> GetOrders(GetAllOrderInput input)
        {
            throw new NotImplementedException();
        }
    }
}
