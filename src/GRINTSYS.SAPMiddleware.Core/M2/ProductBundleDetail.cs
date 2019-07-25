using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ProductBundleDetail: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32? ProductBundleId { get; set; }
        public Int32? ProductVariantId { get; set; }
        public Int32 Quatity { get; set; }
        public Decimal Discount { get; set; }

        public DateTime CreationTime { get; set; }

        public ProductBundleDetail()
        {
            CreationTime = Clock.Now;
        }

        public virtual ProductBundle ProductBundle { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}