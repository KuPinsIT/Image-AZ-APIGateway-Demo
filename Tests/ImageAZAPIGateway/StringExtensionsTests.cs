using ImageAZAPIGateway.Domain.Shared.Utilities;

namespace ImageAZAPIGateway
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("https://example.com/image.jpg", true)]
        [InlineData("http://example.com/image.jpeg", true)]
        [InlineData("https://example.com/image.png", true)]
        [InlineData("https://example.com/image.gif", true)]
        [InlineData("https://example.com/image.bmp", true)]
        [InlineData("https://example.com/image.svg", true)]
        [InlineData("https://example.com/image.webp", true)]
        [InlineData("https://example.com/image.JPG", true)] // Case-insensitivity test
        [InlineData("https://example.com/image.JPEG", true)] // Case-insensitivity test
        [InlineData("https://example.com/image", false)] // Missing extension
        [InlineData("https://example.com/image.txt", false)] // Invalid extension
        [InlineData("ftp://example.com/image.jpg", false)] // Invalid scheme
        [InlineData("/relative/image.jpg", false)] // Relative URL
        [InlineData("", false)] // Empty string
        [InlineData(null, false)] // Null string
        public void IsValidImageUrl_ShouldValidateCorrectly(string url, bool expected)
        {
            // Act
            var result = url.IsValidImageUrl();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}