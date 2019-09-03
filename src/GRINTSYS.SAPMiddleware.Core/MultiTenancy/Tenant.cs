using Abp.MultiTenancy;
using GRINTSYS.SAPMiddleware.Authorization.Users;

namespace GRINTSYS.SAPMiddleware.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public string Logo { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public double ISV { get; set; }
        public string GoogleUA { get; set; }
        public string SAPDatabase { get; set; }
        public string CostingCode { get; set; }
        public string CostingCode2 { get; set; }
    }
}
