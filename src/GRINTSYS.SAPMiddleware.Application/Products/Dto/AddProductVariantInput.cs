﻿using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    [AutoMap(typeof(M2.ProductVariant))]
    public class AddProductVariantInput
    {
        public int TenantId { get; set; }
        public Int32 ItemGroup { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 ColorId { get; set; }
        public Int32 SizeId { get; set; }
        [Required]
        public String Code { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 IsCommitted { get; set; }
        public Double Price { get; set; }
        public String Currency { get; set; }
        public String WareHouseCode { get; set; }
        public String ImageUrl { get; set; }
    }
}
