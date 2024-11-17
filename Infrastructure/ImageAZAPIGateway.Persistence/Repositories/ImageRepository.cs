using ImageAZAPIGateway.Domain.Entities.ImageAggregate;

namespace ImageAZAPIGateway.Persistence.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
