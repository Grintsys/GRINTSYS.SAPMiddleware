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
        public Int32 UserId { get; set; }
        public String Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartProductItem> CartProductItems { get; set; }
        public CartType Type { get; set; }
        public DateTime CreationTime { get; set; }

        public Cart(int userId = 1)
        {
            CreationTime = Clock.Now;
            Type = CartType.CART;
            Currency = "HNL";
            UserId = userId;
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
            return CartProductItems.Sum(s => s.TotalItemPrice());
        }

        public Double GetProductISVPrice()
        {
            return CartProductItems.Sum(s => s.ISV);
        }

        public Double GetProductDiscountPrice()
        {
            return CartProductItems.Sum(s => s.Discount);
        }

        public int GetProductCount()
        {
            return CartProductItems.Count;
        }
    }
}
