namespace ImageAZAPIGateway.Application.Query.Queries.Images.Models
{
    public class ImageSummaryVm
    {
        public int TotalImages { get; set; }
        /// <summary>
        /// Time the count was calculated
        /// </summary>
        public DateTime Timestamp { get; private set; }

        public ImageSummaryVm(int totalImages)
        {
            TotalImages = totalImages;
            Timestamp = DateTime.Now;
        }
    }
}
