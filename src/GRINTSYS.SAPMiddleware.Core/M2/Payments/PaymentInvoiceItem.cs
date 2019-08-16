using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class PaymentInvoiceItem : Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public int PaymentId { get; set; }
        public String DocumentCode { get; set; }
        public Double TotalAmount { get; set; }
        public Double BalanceDue { get; set; }
        public Double PayedAmount { get; set; }
        public DateTime CreationTime { get; set; }
        public Payment Payment { get; set; }
    }
}
