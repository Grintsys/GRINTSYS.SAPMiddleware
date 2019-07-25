using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Configuration.Startup;
using GRINTSYS.SAPMiddleware.Sessions;

namespace GRINTSYS.SAPMiddleware.Web.Views.Shared.Components.SideBarUserArea
{
    public class SideBarUserAreaViewComponent : SAPMiddlewareViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public SideBarUserAreaViewComponent(ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return View(model);
        }
    }
}
