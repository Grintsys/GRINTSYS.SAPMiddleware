using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders
{
    public interface IOrderAppService: IApplicationService, ITransientDependency
    {
        Task CreateOrder(CreateOrderInput input);
        Task GetOrderInSap(SentToSapInput input);
        Task DeleteOrder(DeleteOrderInput input);
        OrderOutput GetOrder(GetOrderInput input);
        PagedResultDto<OrderOutput> GetOrders(GetAllOrderInput input);
        List<SellerOutput> GetSellers(GetAllSellerInput input);
    }
}
