using System;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    public class GetAllPaymentInput
    {
        public int TenantId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}