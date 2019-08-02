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

        public OrderManager(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            return await _orderRepository.InsertAsync(order);
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

        public List<Order> GetOrders(int userId, DateTime begin, DateTime end)
        {
            return _orderRepository.GetAllIncluding(x => x.OrderItems)
                .Where(w => w.UserId == userId
                    && w.CreationTime >= begin && w.CreationTime <= end)
                .ToList();
        }
    }
}
