using System;

namespace GRINTSYS.SAPMiddleware.Banners.Dto
{
    public class GetAllBannerInput
    {
        public int? TenantId { get; set; }
        public String Name { get; set; }
    }
}
