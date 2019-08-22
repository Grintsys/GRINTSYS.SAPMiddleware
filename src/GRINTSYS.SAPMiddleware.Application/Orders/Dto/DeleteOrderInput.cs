using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    public class DeleteOrderInput
    {
        public int? TenantId { get; set; }
        public int OrderId { get; set; }
    }
}
