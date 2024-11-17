using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAZAPIGateway.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        void MarkAsChanged<T>(T entity) where T : Entity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
