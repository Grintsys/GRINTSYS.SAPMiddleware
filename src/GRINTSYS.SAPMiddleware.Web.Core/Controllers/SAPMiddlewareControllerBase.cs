using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace GRINTSYS.SAPMiddleware.Controllers
{
    public abstract class SAPMiddlewareControllerBase: AbpController
    {
        protected SAPMiddlewareControllerBase()
        {
            LocalizationSourceName = SAPMiddlewareConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
