using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class WishlistProductVariant: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Int32 ProductId { get; set; }
        public String Name { get; set; }
        public Int32 CategoryId { get; set; }
        public Double Price { get; set; }
        public Double DiscountPrice { get; set; }
        public String PriceFormatted { get; set; }
        public String DiscountPriceFormatted { get; set; }
        public String Currency { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
        public String MainImage { get; set; }
        public String MainImageHighRes { get; set; }
        public DateTime CreationTime { get; set; }

        public WishlistProductVariant()
        {
            CreationTime = Clock.Now;
        }
    }
}