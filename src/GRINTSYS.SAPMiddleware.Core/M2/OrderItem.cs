using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class OrderItem: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 OrderId { get; set; }
        public String SKU { get; set; }
        public Int32 Quantity { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double TaxValue { get; set; }
        public String TaxCode { get; set; }
        public String WarehouseCode { get; set; }
        
        public DateTime CreationTime { get; set; }

        public OrderItem()
        {
            CreationTime = Clock.Now;
        }

        public virtual Order Order { get; set; }
    }
}