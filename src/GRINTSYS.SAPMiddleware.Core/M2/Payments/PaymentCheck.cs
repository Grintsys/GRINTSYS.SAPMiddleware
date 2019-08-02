using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class PaymentCheck: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 PaymentId { get; set; }
        public String RefenceNumber { get; set; }
        public Int32 BankId { get; set; }
        public Double Amount { get; set; }
        public String GeneralAccount { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Payment Payment { get; set; }

        public DateTime CreationTime { get; set; }

        public PaymentCheck()
        {
            CreationTime = Clock.Now;
        }
    }
}
