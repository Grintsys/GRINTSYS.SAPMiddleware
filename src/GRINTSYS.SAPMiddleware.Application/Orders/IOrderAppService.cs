using Abp.Application.Services;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders
{
    public interface IOrderAppService: IApplicationService, ITransientDependency
    {
        Task CreateOrder(AddOrderInput input);
        Task GetOrderInSap(SentToSapInput input);
        OrderOutput GetOrder(GetOrderInput input);
        List<OrderOutput> GetOrders(GetAllOrderInput input);
        List<SellerOutput> GetSellers(GetAllSellerInput input);
    }
}
