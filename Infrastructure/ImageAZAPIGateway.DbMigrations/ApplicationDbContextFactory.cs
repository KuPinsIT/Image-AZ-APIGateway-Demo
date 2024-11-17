using ImageAZAPIGateway.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ImageAZAPIGateway.DbMigrations
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = args != null && args.Length > 0 ? args[0] : "";
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var configuration = config.Build();
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.SCHEMA);
                sqlOptions.MigrationsAssembly("ImageAZAPIGateway.DbMigrations");
            });

            return new ApplicationDbContext(builder.Options);
        }
    }
}
