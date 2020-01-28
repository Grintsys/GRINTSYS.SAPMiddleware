using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GRINTSYS.SAPMiddleware.M2
{
    public enum PaymentStatus
    {
        CreadoEnAplicacion = 0,
        CreadoEnSAP = 1,
        Error = 2,
        CanceladoPorFinanzas = 3,
        Autorizado = 4
    }
    public enum PaymentType
    {
        Efectivo = 0,
        Cheque = 1,
        Transferencia = 2,
        EfectivoDolar = 3,
        MaquinaPOS = 4
    }

    public class Payment : Entity, IHasCreationTime, IMustHaveTenant
    {
        private const int MaxCommentLength = 150;
        private const int MaxReferenceNumberLength = 50;

        public int TenantId { get; set; }
        public String DocEntry { get; set; }
        public Double PayedAmount { get; set; }
        public String LastMessage { get; set; }
        public PaymentStatus Status { get; set; }
        [StringLength(MaxCommentLength)]
        public String Comment { get; set; }
        [StringLength(MaxReferenceNumberLength)]
        public String ReferenceNumber { get; set; }
        public DateTime CreationTime { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public Int32 BankId { get; set; }
        public PaymentType Type { get; set; }
        public DateTime PayedDate { get; set; }
        public String CardCode { get; set; }
        public virtual User User { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<PaymentInvoiceItem> InvoicesItems { get; set; }
    }
}