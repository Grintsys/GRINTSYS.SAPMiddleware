using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.PurchaseOrders.Dto;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.SapInvoices.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.PurchaseOrders
{
    public interface IPurchaseOrderAppService : IApplicationService, ITransientDependency
    {
        Task CreatePurchaseOrder(GetPurchaseOrderInput input);
        PurchaseOrderOutput GetPurchaseOrder(GetPurchaseOrderInput input);
        Task SendPurchaseOrderToSap(GetPurchaseOrderInput input);      
    }
}