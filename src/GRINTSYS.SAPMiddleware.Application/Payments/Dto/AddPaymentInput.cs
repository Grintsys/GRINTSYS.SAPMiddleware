using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(Payment))]
    public class AddPaymentInput : EntityDto
    {
        private const int MaxCommentLength = 150;
        private const int MaxReferenceNumberLength = 50;
        public int TenantId { get; set; }
        public Int32 InvoiceId { get; set; }
        public Int32 BankId { get; set; }
        public String CardCode { get; set; }
        public Double PayedAmount { get; set; }
        [StringLength(MaxCommentLength)]
        public String Comment { get; set; }
        [StringLength(MaxReferenceNumberLength)]
        public String ReferenceNumber { get; set; }
        public PaymentType Type { get; set; }
        public DateTime PayedDate { get; set; }
        public List<PaymentItemOutput> PaymentItemList { get; set; }
    }
}
