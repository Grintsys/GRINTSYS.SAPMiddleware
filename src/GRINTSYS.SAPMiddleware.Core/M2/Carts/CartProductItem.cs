using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class CartProductItem: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Int32? ProductVariantId { get; set; }
        public Int32 CartId { get; set; }
        public Int32 RemoteId { get; set; }
        //TODO: the best to include orderId
        public int Quantity { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double ISV { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ProductVariant Variant { get; set; }

        public CartProductItem()
        {
            this.CreationTime = Clock.Now;
        }

        public CartProductItem(int tenantId, int cartId, int productVariantId, int qty, double price, double isv, double discount)
        {
            var discountvalue = ((price * qty) * discount) / 100;

            this.TenantId = tenantId;
            this.CreationTime = Clock.Now;
            this.CartId = cartId;
            this.Quantity = qty;
            this.ISV = ((qty * price) - discountvalue) * isv;
            this.Discount = discountvalue;
            this.ProductVariantId = productVariantId;
        }

        public Double TotalItemPrice()
        {
            if (this.Quantity > 0)
                return this.Quantity * this.Variant.Price;

            return 0.0;
        }

        public String TotalItemPriceFormatted()
        {
            return this.Cart.Currency + " " + this.TotalItemPrice();
        }
    }
}