using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    [AutoMapFrom(typeof(M2.Cart))]
    public class CartDto: EntityDto, IHasCreationTime
    {
        public Int32 UserId { get; set; }
        public String Currency { get; set; }
        public CartType Type { get; set; }
        public DateTime CreationTime { get; set; }
    }
}