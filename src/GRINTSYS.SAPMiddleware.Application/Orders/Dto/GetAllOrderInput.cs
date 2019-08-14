using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    public class GetAllOrderInput
    {
        public int TenantId { get; set; }
        public DateTime begin { get; set; }
        public DateTime end { get; set; }
    }
}
