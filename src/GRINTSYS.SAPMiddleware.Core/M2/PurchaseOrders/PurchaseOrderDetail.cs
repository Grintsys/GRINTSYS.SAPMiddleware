using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class PurchaseOrderDetail : Entity
    {
        public int PurchaseOrderId { get; set; }
        public String ItemCode { get; set; }
        public String Dscription { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public String LineCurrency { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineTotal { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
