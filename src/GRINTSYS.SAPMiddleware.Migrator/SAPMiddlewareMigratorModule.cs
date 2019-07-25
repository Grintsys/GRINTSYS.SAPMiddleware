using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GRINTSYS.SAPMiddleware.Configuration;
using GRINTSYS.SAPMiddleware.EntityFrameworkCore;
using GRINTSYS.SAPMiddleware.Migrator.DependencyInjection;

namespace GRINTSYS.SAPMiddleware.Migrator
{
    [DependsOn(typeof(SAPMiddlewareEntityFrameworkModule))]
    public class SAPMiddlewareMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SAPMiddlewareMigratorModule(SAPMiddlewareEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(SAPMiddlewareMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                SAPMiddlewareConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SAPMiddlewareMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
