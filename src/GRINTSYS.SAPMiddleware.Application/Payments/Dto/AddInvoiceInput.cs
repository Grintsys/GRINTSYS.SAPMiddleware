using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(Invoice))]
    public class AddInvoiceInput
    {
        public int TenantId { get; set; }
        public String DocumentCode { get; set; }
        public String DueDate { get; set; }
        public Double TotalAmount { get; set; }
        public Double BalanceDue { get; set; }
        public Int32 ClientId { get; set; }
        public Int32 DocEntry { get; set; }
        public Int32 OverdueDays { get; set; }
    }
}
