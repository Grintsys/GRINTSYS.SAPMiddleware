using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GRINTSYS.SAPMiddleware.Configuration;

namespace GRINTSYS.SAPMiddleware.Web.Startup
{
    [DependsOn(typeof(SAPMiddlewareWebCoreModule))]
    public class SAPMiddlewareWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SAPMiddlewareWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<SAPMiddlewareNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SAPMiddlewareWebMvcModule).GetAssembly());
        }
    }
}
