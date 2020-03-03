using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace GRINTSYS.SAPMiddleware.M2.vwSapInvoices
{
    public interface ISapInvoiceManager : IDomainService
    {
        VwSapInvoice GetSapInvoice(int Id);
        List<VwSapInvoiceDetail> GetSapInvoiceDetail(int Id);
        List<VwSapInvoiceExpense> GetSapInvoiceExpense(int Id);
        List<VwSapInvoice> GetAllSapInvoices();
    }    
}