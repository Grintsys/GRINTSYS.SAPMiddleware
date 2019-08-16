using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    public class GetAllOrderInput
    {
        public int TenantId { get; set; }
        public String begin { get; set; }
        public String end { get; set; }
    }
}
