using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ProductVariant: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Int32 ItemGroup { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 ColorId { get; set; }
        public Int32 SizeId { get; set; }
        [Required]
        public String Code { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 IsCommitted { get; set; }
        public Double Price { get; set; }
        public String Currency { get; set; }
        public String WareHouseCode { get; set; }
        public String ImageUrl { get; set; }
       
        public DateTime CreationTime { get; set; }

        public ProductVariant()
        {
            CreationTime = Clock.Now;
        }

        public Double GetTotal()
        {
            return this.Quantity * this.Price;
        }

        public String GetPriceTotalFormated()
        {
            return String.Format("{0} {1}", this.Currency, this.Price);
        }

        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }
        //[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        //public virtual List<CartProductItem> CartProductVariants { get; set; }
        public virtual List<ProductBundleDetail> ProductBundleDetails { get; set; }
    }
}