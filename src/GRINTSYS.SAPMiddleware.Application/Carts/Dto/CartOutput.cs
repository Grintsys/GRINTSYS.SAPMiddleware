using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class CartOutput
    {
        public int id { get; set; }
        public int product_count { get; set; }
        public double total_price { get; set; }
        public double discount { get; set; }
        public double ISV { get; set; }
        public double subtotal { get; set; }
        public string currency { get; set; }
        public List<CartProductItem> items { get; set; }
    }
}