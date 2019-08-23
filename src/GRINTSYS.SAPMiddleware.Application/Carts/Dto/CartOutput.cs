using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    //[AutoMap(typeof(Cart))]
    public class CartOutput
    {
        public int Id { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get; set; }
        public String TotalPriceFormatted { get; set; }
        public double Discount { get; set; }
        public double ISV { get; set; }
        public double Subtotal { get; set; }
        public string Currency { get; set; }
        public List<CartProductItemOutput> items { get; set; }
        public CartOutput() { }
        public CartOutput(int id) {
            this.Id = id;
            ProductCount = 0;
            TotalPrice = 0;
            Discount = 0;
            ISV = 0;
            Subtotal = 0;
            Currency = "";
        }
    }
}