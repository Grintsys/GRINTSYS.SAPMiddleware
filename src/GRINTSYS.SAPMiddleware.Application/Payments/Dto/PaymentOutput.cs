using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(M2.Payment))]
    public class PaymentOutput: EntityDto
    {
        public int TenantId { get; set; }
        public String DocEntry { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal PayedAmount { get; set; }
        public String LastMessage { get; set; }
        public PaymentStatus Status { get; set; }
        public String StatusDesc { get; set; }
        public String Comment { get; set; }
        public String ReferenceNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public long UserId { get; set; }
        public int BankId { get; set; }
        public PaymentType Type { get; set; }
        public String TypeDesc { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PayedDate { get; set; }
        public String CardCode { get; set; }
        public String CardName { get; set; }
        public String UserName { get; set; }
        public List<PaymentItemOutput> PaymentItemsOutput { get; set; }
    }
}