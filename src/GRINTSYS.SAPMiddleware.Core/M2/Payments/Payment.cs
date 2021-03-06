﻿using Abp.Domain.Entities;
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
        CreadoEnAplicacion = 1,
        CreadoEnSAP = 2,
        Error = 3,
        CanceladoPorFinanzas = 4,
        Autorizado = 5
    }

    public enum PaymentType
    {
        Efectivo = 1,
        Cheque = 2,
        Transferencia = 3
    }

    public class Payment: Entity, IHasCreationTime, IMustHaveTenant
    {
        private const int MaxCommentLength = 150;
        private const int MaxReferenceNumberLength = 50;

        public int TenantId { get; set; }
        public String DocEntry { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public Int32 BankId { get; set; }
        public String CardCode { get; set; }
        //public Int32 ClientId { get; set; }
        public Double PayedAmount { get; set; }
        public String LastMessage { get; set; }
        [StringLength(MaxCommentLength)]
        public String Comment { get; set; }
        [StringLength(MaxReferenceNumberLength)]
        public String ReferenceNumber { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentType Type { get; set; }
        public DateTime PayedDate { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual User User { get; set; }
        public virtual Bank Bank { get; set; }
        //public virtual Client Client { get; set; }
        public virtual ICollection<PaymentInvoiceItem> InvoicesItems { get; set; }

        public Payment()
        {
            Status = PaymentStatus.CreadoEnAplicacion;
            Type = PaymentType.Transferencia;
            CreationTime = Clock.Now;
            PayedDate = Clock.Now;
            LastMessage = "";
            Comment = "";
            PayedAmount = 0.0;
        }
    }
}