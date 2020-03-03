using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class PurchaseOrderExpense : Entity
    {
        public int PurchaseOrderId { get; set; }
        public int ExpnsCode { get; set; }
        public String Comments { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineVat { get; set; }
        public String DistrbMthd { get; set; }
        public Decimal LineTotal { get; set; }
        public String LineCurrency { get; set; }
        public String U_TipoA { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
