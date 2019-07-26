using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class AddCartItemInput
    {
        public int TenantId { get; set; }
        public Int32 CartProductVariantId { get; set; }
        public Int32 CartId { get; set; }
        public Int32 UserId { get; set; }
        //TODO: the best to include orderId
        public Int32? OrderId { get; set; }
        public int Quantity { get; set; }
        //for client discounts
        public String CardCode { get; set; }
    }
}
