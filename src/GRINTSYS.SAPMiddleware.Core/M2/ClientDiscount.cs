using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ClientDiscount: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public String CardCode { get; set; }
        public Int32 ItemGroup { get; set; }
        public double Discount { get; set; }

        public DateTime CreationTime { get; set; }

        public ClientDiscount()
        {
            CreationTime = Clock.Now;
        }
    }
}
