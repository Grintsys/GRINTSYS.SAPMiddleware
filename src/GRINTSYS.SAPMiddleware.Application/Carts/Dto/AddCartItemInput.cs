using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class AddCartItemInput
    {
        public int TenantId { get; set; }
        public Int32 ProductVariantId { get; set; }
        public int Quantity { get; set; }
        //for client discounts
        public String CardCode { get; set; }
    }
}
