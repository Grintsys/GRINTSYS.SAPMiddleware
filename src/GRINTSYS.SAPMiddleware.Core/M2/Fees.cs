using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Fees: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Int32 FeesId { get; set; }
        public Int32 UserId { get; set; }
        public DateTime Date { get; set; }
        public Double Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }

        public Fees()
        {
            CreationTime = Clock.Now;
        }
    }
}
