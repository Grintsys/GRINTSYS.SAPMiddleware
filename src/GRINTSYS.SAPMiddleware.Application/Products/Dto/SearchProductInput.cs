using Abp.Application.Services.Dto;
using System;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class SearchProductInput : IPagedResultRequest, ISortedResultRequest
    {
        public int? TenantId { get; set; }
        public String SearchText { get; set; }
        public int? Category { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public string Sorting { get; set; }

    }
}
