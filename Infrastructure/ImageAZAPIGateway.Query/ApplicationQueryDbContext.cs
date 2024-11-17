using ImageAZAPIGateway.Application.Query.DataModel.Images;
using ImageAZAPIGateway.Application.Query.DataModel.Seedwork;
using ImageAZAPIGateway.Application.Query.Interfaces;
using ImageAZAPIGateway.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace ImageAZAPIGateway.Query
{
    public class ApplicationQueryDbContext : DbContext, IApplicationQueryDbContext
    {
        public const string SCHEMA = "AZAPIGateway";

        private readonly IConnectionStringProvider _connectionStringProvider;
        public bool IncludeDeleted { get; set; }

        public ApplicationQueryDbContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _connectionStringProvider != null)
            {
                var connectionString = _connectionStringProvider.Get();
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>().ToTable(nameof(Image), SCHEMA);

            // Global
            ConfigureGlobal(modelBuilder);
        }

        private void ConfigureGlobal(ModelBuilder modelBuilder)
        {
            var types = modelBuilder.Model.GetEntityTypes();
            foreach (var type in types)
            {
                var configureMethodInfo = typeof(ApplicationQueryDbContext).GetMethod(nameof(ConfigureQueryFilter), BindingFlags.Instance | BindingFlags.NonPublic);
                configureMethodInfo
                    .MakeGenericMethod(type.ClrType)
                    .Invoke(this, [modelBuilder]);
            }
        }

        private void ConfigureQueryFilter<T>(ModelBuilder builder) where T : class
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                Expression<Func<T, bool>> softDeleteFilter = e => IncludeDeleted || !((ISoftDeletable)e).IsDeleted;
                builder.Entity<T>().HasQueryFilter(softDeleteFilter);
            }
        }
    }
}
