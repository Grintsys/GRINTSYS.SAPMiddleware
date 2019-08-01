using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class GetAllProductInput: PagedAndSortedResultRequestDto
    {
        public int? TenantId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Int32? CategoryId { get; set; }
        public Int32? BrandId { get; set; }
        public String Description { get; set; }
    }
}
