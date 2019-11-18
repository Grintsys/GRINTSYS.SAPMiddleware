using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class VwSapInvoice : Entity
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
        public Decimal DocTotalExp { get; set; }        
        public String DocCurrency { get; set; }
        public String Comments { get; set; }
        public int SlpCode { get; set; }
    }
}
