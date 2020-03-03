using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GRINTSYS.SAPMiddleware.Configuration;

namespace GRINTSYS.SAPMiddleware.Web.Host.Startup
{
    [DependsOn(
       typeof(SAPMiddlewareWebCoreModule))]
    public class SAPMiddlewareWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SAPMiddlewareWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SAPMiddlewareWebHostModule).GetAssembly());
        }
    }
}
