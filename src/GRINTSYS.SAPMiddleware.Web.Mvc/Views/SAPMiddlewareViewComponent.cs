using Abp.AspNetCore.Mvc.ViewComponents;

namespace GRINTSYS.SAPMiddleware.Web.Views
{
    public abstract class SAPMiddlewareViewComponent : AbpViewComponent
    {
        protected SAPMiddlewareViewComponent()
        {
            LocalizationSourceName = SAPMiddlewareConsts.LocalizationSourceName;
        }
    }
}
