using Ardalis.Specification;
using ImageAZAPIGateway.Domain.Entities.ImageAggregate;

namespace ImageAZAPIGateway.Application.Command.Features.Images.Specifications
{
    public class ImageByUrlSpec : Specification<Image>, ISingleResultSpecification<Image>
    {
        public ImageByUrlSpec(string url)
        {
            Query
                .Where(n => n.Url.Trim() == url.Trim());
        }
    }
}
