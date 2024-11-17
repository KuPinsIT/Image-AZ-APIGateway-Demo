using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ImageAZAPIGateway.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace ImageAZAPIGateway.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, IAggregateRoot
    {
        protected readonly ApplicationDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        protected Repository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().FindAsync(id, cancellationToken).AsTask();
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(specification);
            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public Task<int> CountAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(specification);
            return query.CountAsync(cancellationToken);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
