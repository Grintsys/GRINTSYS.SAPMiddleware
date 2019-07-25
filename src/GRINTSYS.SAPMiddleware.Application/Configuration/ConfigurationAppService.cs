using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GRINTSYS.SAPMiddleware.Configuration.Dto;

namespace GRINTSYS.SAPMiddleware.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SAPMiddlewareAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
