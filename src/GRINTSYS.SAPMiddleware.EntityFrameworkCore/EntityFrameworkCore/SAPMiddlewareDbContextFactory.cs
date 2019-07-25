using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GRINTSYS.SAPMiddleware.Configuration;
using GRINTSYS.SAPMiddleware.Web;

namespace GRINTSYS.SAPMiddleware.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SAPMiddlewareDbContextFactory : IDesignTimeDbContextFactory<SAPMiddlewareDbContext>
    {
        public SAPMiddlewareDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SAPMiddlewareDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SAPMiddlewareDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SAPMiddlewareConsts.ConnectionStringName));

            return new SAPMiddlewareDbContext(builder.Options);
        }
    }
}
