using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductOutput
    {
        public const int MaxNameLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB
        public String Name { get; set; }
        public String Code { get; set; }
        public Int32 CategoryId { get; set; }
        public Int32 BrandId { get; set; }
        public String Season { get; set; }
        public String Description { get; set; }
        public String MainImage { get; set; }
        public String MainImageHighRes { get; set; }
        public List<ProductVariantOutput> Variants { get; set; }
    }

}
