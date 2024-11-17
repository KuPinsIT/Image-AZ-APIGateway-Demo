using ImageAZAPIGateway.Domain.Shared;

namespace ImageAZAPIGateway.Application.Query.DataModel.Images
{
    public class Image : IAuditable
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
