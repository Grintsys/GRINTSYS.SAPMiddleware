using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    public class AddOrderInput
    {
        public int TenantId { get; set; }
        public String CardCode { get; set; }
        public String Comment { get; set; }
        public String DeliveryDate { get; set; }
    }
}
