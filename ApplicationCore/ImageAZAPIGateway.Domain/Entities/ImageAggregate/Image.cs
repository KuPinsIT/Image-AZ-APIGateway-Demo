using EnsureThat;
using ImageAZAPIGateway.Domain.Seedwork;
using ImageAZAPIGateway.Domain.Shared;
using ImageAZAPIGateway.Domain.Shared.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ImageAZAPIGateway.Domain.Entities.ImageAggregate
{
    public class Image : Entity, IAggregateRoot, IAuditable
    {
        public string Url { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }

        private Image() { }

        public Image(string url, string description)
        {
            Ensure.That(url, nameof(url)).IsNotEmptyOrWhiteSpace();
            Ensure.That(description, nameof(description)).IsNotEmptyOrWhiteSpace();
            if (!url.IsValidImageUrl())
            {
                throw new ValidationException($"Image url '{url}' is invalid");
            }
            Id = Guid.NewGuid();
            Url = url;
            Description = description;
        }
    }
}
