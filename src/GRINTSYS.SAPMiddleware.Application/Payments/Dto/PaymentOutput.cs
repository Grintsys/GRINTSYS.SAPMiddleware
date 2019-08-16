using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(M2.Payment))]
    public class PaymentOutput: EntityDto
    {
        public int TenantId { get; set; }
        public String DocEntry { get; set; }
        public long UserId { get; set; }
        public int DebtCollectorId { get; set; }
        public String InvoiceNumber { get; set; }
        public String BankName { get; set; }
        public String GeneralAccount { get; set; }
        public Double PayedAmount { get; set; }
        public String LastErrorMessage { get; set; }
        public String Comment { get; set; }
        public String ReferenceNumber { get; set; }
        public String Status { get; set; }
        public String Type { get; set; }
        public DateTime CreationTime { get; set; }
        //public virtual List<PaymentItemOutput> InvoicesItems { get; set; }
    }
}
