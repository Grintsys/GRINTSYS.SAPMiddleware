using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class DeletePaymentInput
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }
    }
}
