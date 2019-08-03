using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    public class PaymentInput
    {
        public int TenantId { get; set; }
        public string Comment { get; set; }
        public double Total { get; set; }
    }
}
