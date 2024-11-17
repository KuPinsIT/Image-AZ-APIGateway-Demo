using EnsureThat;
using ImageAZAPIGateway.Application.Common.Shared;
using ImageAZAPIGateway.Domain.Seedwork;
using ImageAZAPIGateway.Domain.Shared;
using ImageAZAPIGateway.Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace ImageAZAPIGateway.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public const string SCHEMA = "AZAPIGateway";

        private readonly IMediator _mediator;
        private readonly ApplicationContext _applicationContext;
        private readonly IConnectionStringProvider _connectionStringProvider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(ApplicationContext applicationContext, IMediator mediator, IConnectionStringProvider connectionStringProvider)
        {
            _mediator = mediator;
            _applicationContext = applicationContext;
            _connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _connectionStringProvider != null)
            {
                var connectionString = _connectionStringProvider.Get();
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", SCHEMA);
                    });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEntities(modelBuilder);
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        private static void ConfigureEntities(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void MarkAsChanged<T>(T entity) where T : Entity
        {
            Entry(entity).State = EntityState.Modified;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_mediator != null)
            {
                await _mediator.DispatchDomainEventsAsync(this);
            }

            PreSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);

            //TODO: Handle concurrency check
        }

        private void PreSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.Entity is Entity entity)
                {
                    if (entity.IsTransient)
                    {
                        entry.State = EntityState.Added;
                    }
                }

                if ((entry.State == EntityState.Added || entry.State == EntityState.Modified) && (entry.Entity is IAuditable || entry.Entity is ICreationAuditable))
                {
                    HandleAudit(entry);
                }

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable)
                {
                    HandleSoftDelete(entry);
                }

                //TODO: Handle concurrency check
            }
        }

        private void HandleAudit(EntityEntry entry)
        {
            if (entry.Entity is IAuditable auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedBy = _applicationContext.Principal.UserId;
                    auditable.CreatedDate = DateTime.UtcNow;
                }
                else
                {
                    auditable.LastUpdatedBy = _applicationContext.Principal.UserId;
                    auditable.LastUpdated = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is ICreationAuditable creationAuditable)
            {
                if (entry.State == EntityState.Added)
                {
                    creationAuditable.CreatedBy = _applicationContext.Principal.UserId;
                    creationAuditable.CreatedDate = DateTime.UtcNow;
                }
            }
        }

        private void HandleSoftDelete(EntityEntry entry)
        {
            Ensure.That(_applicationContext.Principal, optsFn: o => o.WithMessage("Auditing failed, ApplicationContext Principal is null")).IsNotNull();

            entry.Property("IsDeleted").CurrentValue = true;
            entry.Property("DeletedBy").CurrentValue = _applicationContext.Principal.UserId;
            entry.Property("DeletedDate").CurrentValue = DateTime.UtcNow;
            entry.State = EntityState.Modified;
        }
    }
}
