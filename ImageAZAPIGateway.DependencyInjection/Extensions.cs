using ImageAZAPIGateway.DbMigrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAZAPIGateway.DependencyInjection
{
    public static class Extensions
    {
        public static void MigrateDb(this IApplicationBuilder app, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var ctx = new ApplicationDbContextFactory().CreateDbContext([connectionString]);
            ctx.Migrate();
        }
    }
}
