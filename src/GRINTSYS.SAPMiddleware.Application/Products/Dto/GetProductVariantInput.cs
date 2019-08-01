using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class GetProductVariantInput
    {
        public int TenantId { get; set; }
        public int productId { get; set; }
    }
}
