using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    [AutoMap(typeof(M2.Size))]
    public class SizeOuput: EntityDto
    {
        public int TenantId { get; set; }
        public String Value { get; set; }
        public String Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}