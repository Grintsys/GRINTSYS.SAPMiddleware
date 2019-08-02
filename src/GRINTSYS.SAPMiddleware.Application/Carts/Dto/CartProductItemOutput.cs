using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    [AutoMap(typeof(CartProductItem))]
    public class CartProductItemOutput
    {
        public Int32 Id { get; set; }
        public int Quantity { get; set; }
        public Int32 RemoteId { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double ISV { get; set; }
        public Double TotalItemPrice { get; set; }
        public String TotalItemPriceFormatted { get; set; }
        public ProductVariant Variant { get; set; }
    }
}