using System;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    public class GetAllPaymentInput
    {
        public int TenantId { get; set; }
        public String Begin { get; set; }
        public String End { get; set; }
    }
}