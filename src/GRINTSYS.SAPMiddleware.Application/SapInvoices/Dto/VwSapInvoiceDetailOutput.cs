using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.SapInvoices.Dto
{
    [AutoMapFrom(typeof(M2.VwSapInvoiceDetail))]
    public class VwSapInvoiceDetailOutput : EntityDto
    {
        public int VwSapInvoiceId { get; set; }
        public String ItemCode { get; set; }
        public String Dscription { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public String LineCurrency { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineTotal { get; set; }
    }
}