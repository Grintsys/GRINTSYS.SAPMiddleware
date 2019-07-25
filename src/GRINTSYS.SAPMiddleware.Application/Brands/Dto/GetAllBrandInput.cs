using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Brands.Dto
{
    public class GetAllBrandInput
    {
        public int? TenantId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
    }
}
