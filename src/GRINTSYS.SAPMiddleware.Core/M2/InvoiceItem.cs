using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class InvoiceItem : Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Int32? PaymentId { get; set; }
        public Int32 DocEntry { get; set; }
        public String DocumentNumber { get; set; }
        public Double TotalAmount { get; set; }
        public Double PayedAmount { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual Payment Payment { get; set; }

        public InvoiceItem()
        {
            CreationTime = Clock.Now;
        }
    }
}