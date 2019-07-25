using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Sizes.Dto
{
    [AutoMap(typeof(M2.Size))]
    public class SizeDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxValueLength = 24;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxValueLength)]
        public String Value { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }

        public DateTime CreationTime { get; set; }
    }

}
