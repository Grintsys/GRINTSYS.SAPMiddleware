using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class AddCartInput
    {
        public int TenantId { get; set; }
        public Int32 UserId { get; set; }
        public Double TotalPrice { get; set; }
        public String TotalPriceFormatted { get; set; }
        public String Currency { get; set; }
        public int Type { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
