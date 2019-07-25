using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Banner: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String Name { get; set; }
        public String Target { get; set; }
        public String ImageUrl { get; set; }
        public DateTime CreationTime { get; set; }
        public Banner()
        {
            CreationTime = Clock.Now;
        }
    }
}
