using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GRINTSYS.SAPMiddleware.EntityFrameworkCore
{
    public static class SAPMiddlewareDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SAPMiddlewareDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SAPMiddlewareDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
