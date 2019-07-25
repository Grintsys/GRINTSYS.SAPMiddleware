using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ProductBundle: Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;

        public int TenantId { get; set; }

        [StringLength(MaxNameLength)]
        public String Name { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual ICollection<ProductBundleDetail> ProductBundleDetails { get; set; }

        public ProductBundle()
        {
            CreationTime = Clock.Now;
        }
    }
}
