using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace GRINTSYS.SAPMiddleware.Web.Views
{
    public abstract class SAPMiddlewareRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected SAPMiddlewareRazorPage()
        {
            LocalizationSourceName = SAPMiddlewareConsts.LocalizationSourceName;
        }
    }
}
