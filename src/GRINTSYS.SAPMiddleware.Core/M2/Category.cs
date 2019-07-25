using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Category: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 PartentId { get; set; }
        public Int32 RemoteId { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }

        public DateTime CreationTime { get; set; }

        public Category()
        {
            CreationTime = Clock.Now;
        }
    }
}