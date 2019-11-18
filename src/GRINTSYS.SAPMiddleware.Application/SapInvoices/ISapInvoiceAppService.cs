using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.SapInvoices.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GRINTSYS.SAPMiddleware.SapInvoices
{
    public interface ISapInvoiceAppService : IApplicationService, ITransientDependency
    {
        PagedResultDto<VwSapInvoiceOutput> GetAllSapInvoices();
        VwSapInvoiceOutput GetSapInvoice(GetSapInvoiceInput input);
        PagedResultDto<VwSapInvoiceDetailOutput> GetSapInvoiceDetail(GetSapInvoiceInput input);
    }
}