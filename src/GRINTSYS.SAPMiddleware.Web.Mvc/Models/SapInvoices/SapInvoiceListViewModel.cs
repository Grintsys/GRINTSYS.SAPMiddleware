using GRINTSYS.SAPMiddleware.SapInvoices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Web.Models.SapInvoices
{
    public class SapInvoiceListViewModel
    {
        public IReadOnlyList<VwSapInvoiceOutput> SapInvoices { get; set; }
    }
}
