using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Brand: Entity, IHasCreationTime, IMustHaveTenant
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

        public Brand()
        {
            CreationTime = Clock.Now;
        }
    }
}