using FluentAssertions;
using ImageAZAPIGateway.Domain.Entities.ImageAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAZAPIGateway.UnitTests.DomainTests
{
    public class ImageTests
    {
        [Fact]
        public void Constructor_ShouldThrowValidationException_WhenUrlIsInvalid()
        {
            // Arrange
            var invalidUrl = "invalid-url";
            var description = "Valid description";

            // Act & Assert
            Action act = () => new Image(invalidUrl, description);

            act.Should().Throw<ValidationException>()
                .WithMessage($"Image url '{invalidUrl}' is invalid");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenUrlIsEmpty()
        {
            // Arrange
            var emptyUrl = string.Empty;
            var description = "Valid description";

            // Act & Assert
            Action act = () => new Image(emptyUrl, description);

            act.Should().Throw<ArgumentException>()
                .WithMessage("The string can't be empty or consist of only whitespace characters. (Parameter 'url')");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenDescriptionIsEmpty()
        {
            // Arrange
            var url = "http://validurl.com";
            var emptyDescription = string.Empty;

            // Act & Assert
            Action act = () => new Image(url, emptyDescription);

            act.Should().Throw<ArgumentException>()
                .WithMessage("The string can't be empty or consist of only whitespace characters. (Parameter 'description')");
        }

        [Fact]
        public void Constructor_ShouldCreateImage_WhenUrlAndDescriptionAreValid()
        {
            // Arrange
            var validUrl = "http://validurl.png";
            var validDescription = "Valid description";

            // Act
            var image = new Image(validUrl, validDescription);

            // Assert
            image.Url.Should().Be(validUrl);
            image.Description.Should().Be(validDescription);
            image.Id.Should().NotBe(Guid.Empty);
        }
    }
}
