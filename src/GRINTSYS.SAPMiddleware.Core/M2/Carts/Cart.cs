using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public enum CartType
    {
        CART = 0,
        FAVORITES = 1
    };

    public class Cart: Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;

        public int TenantId { get; set; }
        public long UserId { get; set; }
        public String Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartProductItem> CartProductItems { get; set; }
        public CartType Type { get; set; }
        public DateTime CreationTime { get; set; }

        public Cart(int tenantId, long userId = 1)
        {
            CreationTime = Clock.Now;
            Type = CartType.CART;
            UserId = userId;
            TenantId = tenantId;
        }

        public String GetProductTotalPriceFormatted()
        {
            return this.Currency + (GetProductSubtotalPrice() - GetProductDiscountPrice()) + GetProductISVPrice();
        }

        public Double GetProductTotalPrice()
        {
            return (GetProductSubtotalPrice() - GetProductDiscountPrice()) + GetProductISVPrice();
        }
        public Double GetProductSubtotalPrice()
        {
            if (CartProductItems.Count <= 0)
                return 0;

            return CartProductItems.Sum(s => s.Quantity * s.Variant.Price);
        }

        public Double GetProductISVPrice()
        {
            if (CartProductItems.Count <= 0)
                return 0;

            return CartProductItems.Sum(s => s.ISV);
        }

        public Double GetProductDiscountPrice()
        {
            if (CartProductItems.Count <= 0)
                return 0;

            return CartProductItems.Sum(s => s.Discount);
        }

        public int GetProductCount()
        {
            return CartProductItems.Count;
        }
    }
}
