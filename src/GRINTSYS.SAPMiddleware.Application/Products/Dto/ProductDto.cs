using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    [AutoMap(typeof(M2.Product))]
    public class ProductDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        [Required]
        public String Code { get; set; }
        public Int32 CategoryId { get; set; }
        public Int32 BrandId { get; set; }
        public String Season { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }
        public String MainImage { get; set; }
        public String MainImageHighRes { get; set; }

        public DateTime CreationTime { get; set; }
    }

}
