using Microsoft.EntityFrameworkCore;

namespace ImageAZAPIGateway.Application.Query.Interfaces
{
    public interface IApplicationQueryDbContext
    {
        DbSet<T> Set<T>() where T : class;
        public bool IncludeDeleted { get; set; }
    }
}
