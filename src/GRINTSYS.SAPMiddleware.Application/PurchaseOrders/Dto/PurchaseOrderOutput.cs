using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.PurchaseOrders.Dto
{
    [AutoMapFrom(typeof(M2.PurchaseOrder))]
    public class PurchaseOrderOutput : EntityDto
    {
        public int TenantId { get; set; }
        public int RemoteId { get; set; }
        public int Status { get; set; }
        public String LastMessage { get; set; }
        public long UserId { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public DateTime DocCreateDate { get; set; }
        public DateTime DocDate { get; set; }
        public String CardCode { get; set; }
        public String CardName { get; set; }
        public Decimal DocTotal { get; set; }
        public String DocCurrency { get; set; }
        public String Comments { get; set; }
        public int SlpCode { get; set; }

    }
}