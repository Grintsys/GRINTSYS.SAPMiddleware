using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class AddProductVariantInput
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
    }
}
