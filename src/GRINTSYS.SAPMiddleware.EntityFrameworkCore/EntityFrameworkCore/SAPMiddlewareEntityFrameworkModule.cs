using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using GRINTSYS.SAPMiddleware.EntityFrameworkCore.Seed;

namespace GRINTSYS.SAPMiddleware.EntityFrameworkCore
{
    [DependsOn(
        typeof(SAPMiddlewareCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class SAPMiddlewareEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<SAPMiddlewareDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SAPMiddlewareDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SAPMiddlewareDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SAPMiddlewareEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
