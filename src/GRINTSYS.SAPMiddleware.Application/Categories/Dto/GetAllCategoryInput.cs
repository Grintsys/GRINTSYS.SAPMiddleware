using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Categories.Dto
{
    public class GetAllCategoryInput
    {
        public int? TenantId { get; set; }
        public Int32? PartentId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public String Type { get; set; }

    }
}
