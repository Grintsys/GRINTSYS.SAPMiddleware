using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Orders
{
    public interface IOrderManager: IDomainService
    {
        Task<Order> CreateOrder(Order order);
        Task CreateOrderItem(OrderItem orderItem);
        Order GetOrder(int id);
        Order UpdateOrder(Order order);
        List<Order> GetOrders(int userId, DateTime begin, DateTime end);
    }
}
