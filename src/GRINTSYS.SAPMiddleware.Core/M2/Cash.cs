using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Cash : Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public String GeneralAccount { get; set; }
        public Double Amount { get; set; }

        public DateTime CreationTime { get; set; }

        public Cash()
        {
            CreationTime = Clock.Now;
        }
    }
}
