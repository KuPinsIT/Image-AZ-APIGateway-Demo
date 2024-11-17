using Ardalis.Specification;

namespace ImageAZAPIGateway.Domain.Seedwork
{
    public interface IRepository<T> where T : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(T entity);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        void Remove(T entity);
        Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    }
}
