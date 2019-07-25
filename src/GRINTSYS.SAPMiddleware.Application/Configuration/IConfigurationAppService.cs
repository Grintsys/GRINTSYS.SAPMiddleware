using System.Threading.Tasks;
using GRINTSYS.SAPMiddleware.Configuration.Dto;

namespace GRINTSYS.SAPMiddleware.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
