using AutoMapper;
using ImageAZAPIGateway.Application.Query.DataModel.Images;

namespace ImageAZAPIGateway.Application.Query.Queries.Images.Models
{
    public class ImageVm
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Image, ImageVm>();
            }
        }
    }
}
