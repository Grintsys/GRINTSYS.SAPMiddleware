using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Clients.Dto
{

    [AutoMap(typeof(M2.Invoice))]
    public class InvoiceDto : EntityDto
    {
        public int TenantId { get; set; }
        public int DoctEntry { get; set; }
        public String DocumentCode { get; set; }
        public String DueDate { get; set; }
        public Double TotalAmount { get; set; }
        public Double BalanceDue { get; set; }
        public Int32 ClientId { get; set; }
        public Int32 DocEntry { get; set; }
        public double OverdueDays { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
