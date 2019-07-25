using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Brands.Dto
{
    public class BrandDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        public String Code { get; set; }
        public String BrandImg { get; set; }
        public bool IsPremium { get; set; }

        public DateTime CreationTime { get; set; }
    }

}
