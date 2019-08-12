using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Clients.Dto
{
    public class ClientSearchInput : IPagedResultRequest, ISortedResultRequest
    {
        public int? TenantId { get; set; }
        public String SearchText { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public string Sorting { get; set; }
    }
}
