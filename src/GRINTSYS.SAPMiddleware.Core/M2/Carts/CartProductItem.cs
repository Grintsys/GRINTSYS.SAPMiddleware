using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
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
        public int Quantity { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double ISV { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual CartProductVariant CartProductVariant { get; set; }

        public CartProductItem()
        {
            this.CreationTime = Clock.Now;
        }

        public CartProductItem(int cartId, int qty, double isv, double discount)
        {
            this.CreationTime = Clock.Now;
            this.CartId = cartId;
            this.Quantity = qty;
            this.ISV = isv;
            this.Discount = discount;
        }

        public Double TotalItemPrice()
        {
            if (this.Quantity > 0)
                return this.Quantity * this.CartProductVariant.Price;

            return 0;
        }

        public String TotalItemPriceFormatted()
        {
            return this.Cart.Currency + " " + this.TotalItemPrice();
        }
    }
}