using System.Text.RegularExpressions;

namespace ImageAZAPIGateway.Domain.Shared.Utilities
{
    public static class Extensions
    {
        public static bool IsValidImageUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            // Regex to match common image file extensions
            string pattern = @"^https?:\/\/.*\.(jpg|jpeg|png|gif|bmp|svg|webp)$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Check if the URL matches the regex
            return regex.IsMatch(url);
        }
    }
}
