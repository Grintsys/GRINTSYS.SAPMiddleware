using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class VwSapInvoiceDetail : Entity
    {
        public int VwSapInvoiceId { get; set; }
        public String ItemCode { get; set; }
        public String Dscription { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public String LineCurrency { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineTotal { get; set; }
    }
}
