using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Categories.Dto
{
    [AutoMap(typeof(M2.Category))]
    public class CategoryDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;

        public int TenantId { get; set; }

        public Int32? PartentId { get; set; }
        public Int32 RemoteId { get; set; }

        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        public String Type { get; set; }

        public DateTime CreationTime { get; set; }
    }

}
