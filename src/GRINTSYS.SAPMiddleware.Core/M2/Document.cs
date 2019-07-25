using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Document: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public String DocumentCode { get; set; }
        public String DueDate { get; set; }
        public Double TotalAmount { get; set; }
        public Double PayedAmount { get; set; }
        public Double BalanceDue { get; set; }
        public Int32 ClientId { get; set; }
        public Int32 DocEntry { get; set; }
        public Int32 OverdueDays { get; set; }
        public virtual Client Client { get; set; }

        public DateTime CreationTime { get; set; }

        public Document()
        {
            CreationTime = Clock.Now;
        }
    }
}
