using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(Payment))]
    public class UpdatePaymentInput : EntityDto
    {
        private const int MaxCommentLength = 150;
        private const int MaxReferenceNumberLength = 50;
        
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public Double PayedAmount { get; set; }
        [StringLength(MaxCommentLength)]
        public String Comment { get; set; }
        [StringLength(MaxReferenceNumberLength)]
        public String ReferenceNumber { get; set; }
        public Int32 BankId { get; set; }
        public PaymentType Type { get; set; }
        public DateTime PayedDate { get; set; }
        public String CardCode { get; set; }
        public string[] Invoices { get; set; }
    }
}