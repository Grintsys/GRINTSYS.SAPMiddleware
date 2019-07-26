using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class CartProductItem: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 CartProductVariantId { get; set; }
        public Int32 CartId { get; set; }
        public Int32 RemoteId { get; set; }
        //TODO: the best to include orderId
        public Int32? OrderId { get; set; }
        public int Quantity { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double ISV { get; set; }
        public Double TotalItemPrice { get; set; }
        public String TotalItemPriceFormatted { get; set; }
        public int Expiration { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Order Order { get; set; }
        public virtual CartProductVariant CartProductVariant { get; set; }
        public int Type { get; set; }
    }
}