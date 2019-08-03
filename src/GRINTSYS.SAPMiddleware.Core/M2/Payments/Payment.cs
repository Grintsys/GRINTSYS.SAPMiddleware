using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.M2
{
    public enum PaymentStatus
    {
        CreadoEnAplicacion = 1,
        CreadoEnSAP = 2,
        Error = 3,
        Cancelado = 4
    }

    public class Payment: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String DocEntry { get; set; }
        public long UserId { get; set; }
        public Double TotalAmount { get; set; }
        public String LastErrorMessage { get; set; }
        public PaymentStatus Status { get; set; }
        public String Comment { get; set; }
        public String ReferenceNumber { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual List<InvoiceItem> Invoices { get; set; }
        public virtual List<PaymentCheck> Checks { get; set; }
        public virtual List<PaymentCash> Cash { get; set; }
        public virtual List<PaymentTransfer> Transfer { get; set; }
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }

        public Payment()
        {
            Status = PaymentStatus.CreadoEnAplicacion;
            CreationTime = Clock.Now;
            LastErrorMessage = "";
        }
    }
}