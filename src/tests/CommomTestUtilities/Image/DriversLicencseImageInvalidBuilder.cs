using SixLabors.ImageSharp;

namespace CommomTestUtilities.Image
{
    public class DriversLicencseImageInvalidBuilder
    {
        public static MemoryStream Build()
        {
            var image = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(100, 100);
            var memoryStream = new MemoryStream();
            image.SaveAsGif(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
