using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using GRINTSYS.SAPMiddleware.Authorization.Roles;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.Configuration;
using GRINTSYS.SAPMiddleware.Localization;
using GRINTSYS.SAPMiddleware.MultiTenancy;
using GRINTSYS.SAPMiddleware.Timing;

namespace GRINTSYS.SAPMiddleware
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SAPMiddlewareCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            SAPMiddlewareLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = SAPMiddlewareConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SAPMiddlewareCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
