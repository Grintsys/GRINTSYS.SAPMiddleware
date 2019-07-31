using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Colors.Dto
{
    [AutoMap(typeof(M2.Color))]
    public class ColorDto: EntityDto, IMustHaveTenant
    {
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        public int TenantId { get; set; }

        public Int32 RemoteId { get; set; }
        public String Value { get; set; }
        public String Code { get; set; }
        public String Image { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }

       

    }

}
