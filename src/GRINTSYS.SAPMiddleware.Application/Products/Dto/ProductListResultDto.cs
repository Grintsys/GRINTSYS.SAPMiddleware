using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class ProductListResultDto: PagedResultDto<ProductOutput>
    {
        public ProductMetadataOutput Metadata { get; set; }

        public ProductListResultDto()
        {
            //I'm tesing a new feature, i can do permanent filters on app screen
            this.Metadata = new ProductMetadataOutput();
            this.Metadata.filters = new List<FilterOutput>();

            var item = new FilterOutput()
            {
                Id = 1,
                Label = "Price",
                Name = "Price Range",
                Type = "range",
                Min = AppConsts.MinFilter,
                Max = AppConsts.MaxFilter
            };

            this.Metadata.filters.Add(item);
        }
    }
}
