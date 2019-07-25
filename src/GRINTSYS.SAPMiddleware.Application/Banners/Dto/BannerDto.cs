using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace GRINTSYS.SAPMiddleware.Banners.Dto
{
    [AutoMap(typeof(M2.Banner))]
    public class BannerDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String Name { get; set; }
        public String Target { get; set; }
        public String ImageUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }

}
