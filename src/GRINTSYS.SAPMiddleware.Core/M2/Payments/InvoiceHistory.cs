using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class InvoiceHistory: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String DocNum { get; set; }
        public String CardCode { get; set; }
        public String CardName { get; set; }
        public Double Total { get; set; }
        public DateTime CreationTime { get; set; }

        public InvoiceHistory()
        {
            CreationTime = Clock.Now;
        }
    }
}
