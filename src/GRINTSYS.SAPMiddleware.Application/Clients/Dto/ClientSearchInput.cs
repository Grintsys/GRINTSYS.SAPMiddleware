using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Clients.Dto
{
    public class ClientSearchInput
    {
        public int? TenantId { get; set; }
        public String SearchText { get; set; }
    }
}
