using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;

namespace GRINTSYS.SAPMiddleware
{
    [DependsOn(
        typeof(SAPMiddlewareCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SAPMiddlewareApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SAPMiddlewareAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SAPMiddlewareApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
