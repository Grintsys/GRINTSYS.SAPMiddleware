using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GRINTSYS.SAPMiddleware.M2
{
    public class Product : Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        [Required]
        public String Code { get; set; }
        public Int32 CategoryId { get; set; }
        public Int32 BrandId { get; set; }
        public String Season { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }
        public String MainImage { get; set; }
        public String MainImageHighRes { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductVariant> Variants { get; set; }

        public Product()
        {
            CreationTime = Clock.Now;
        }
    }
}
