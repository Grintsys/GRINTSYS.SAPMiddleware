using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Orders
{
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Cart> _cartRespository;

        public OrderManager(IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Cart> cartRespository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRespository = cartRespository;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var cart = _cartRespository.GetAll()
                .Where(w => w.UserId == order.UserId
                    && w.TenantId == order.TenantId)
                .FirstOrDefault();

            if(cart == null)
            {
                throw new UserFriendlyException("You can't create a new Order because your cart is empty");
            }

            return await _orderRepository.InsertAsync(order);
        }

        public Task CreateOrderItem(OrderItem orderItem)
        {
            return _orderItemRepository.InsertAsync(orderItem);
        }

        public Order GetOrder(int id)
        {
            var order = _orderRepository.GetAllIncluding(x => x.OrderItems)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if(order == null)
            {
                throw new UserFriendlyException("Order not found");
            }

            return order;
        }

        public List<Order> GetOrders(int tenantId, long userId, DateTime begin, DateTime end)
        {
            return _orderRepository.GetAllIncluding(x => x.OrderItems)
                .Where(w => w.TenantId == tenantId
                    && w.UserId == userId
                    && w.CreationTime >= begin && w.CreationTime <= end)
                .ToList();
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepository.Update(order);
        }
    }
}
