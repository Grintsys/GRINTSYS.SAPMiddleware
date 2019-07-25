using System;

namespace GRINTSYS.SAPMiddleware.Clients.Dto
{
    public class GetAllClientInput
    {
        public int? TenantId { get; set; }
        public String Name { get; set; }
        public String CardCode { get; set; }
        public String RTN { get; set; }
    }
}
