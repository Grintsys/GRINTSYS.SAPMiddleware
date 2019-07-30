using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ClientTransaction: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 ReferenceNumber { get; set; }
        public String CardCode { get; set; }
        public String Description { get; set; }
        public Double Amount { get; set; }

        public DateTime CreationTime { get; set; }

        public ClientTransaction()
        {
            CreationTime = Clock.Now;
        }
    }
}
