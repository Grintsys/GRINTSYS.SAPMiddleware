using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class VwSapInvoiceExpense : Entity
    {
        public int VwSapInvoiceId { get; set; }
        public int ExpnsCode { get; set; }
        public String Comments { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineVat { get; set; }
        public String DistrbMthd { get; set; }
        public Decimal LineTotal { get; set; }
        public String LineCurrency { get; set; }
        public String U_TipoA { get; set; }
    }
}
