﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Job
{
    [Serializable]
    public class OrderParams
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }
        public String CardCode { get; set; }
        public String Comment { get; set; }
        public String DeliveryDate { get; set; }
    }
}
