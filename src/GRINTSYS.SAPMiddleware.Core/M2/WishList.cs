using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class WishList: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32? WishlistProductVariantId { get; set; }
        public Int32? DeviceUserId { get; set; }

        public WishlistProductVariant WishlistProductVariant { get; set; }
        public User User { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public WishList()
        {
            CreationTime = Clock.Now;
        }
    }
}
