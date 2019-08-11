using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    [AutoMap(typeof(M2.Color))]
    public class ColorOutput : EntityDto
    {
        public int TenantId { get; set; }
        public Int32 RemoteId { get; set; }
        public String Value { get; set; }
        public String Code { get; set; }
        public String Image { get; set; }
        public String Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}