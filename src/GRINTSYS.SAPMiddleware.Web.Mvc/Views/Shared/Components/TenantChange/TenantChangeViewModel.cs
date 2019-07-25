using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.Sessions.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
