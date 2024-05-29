using SixLabors.ImageSharp;

namespace CommomTestUtilities.Image
{
    public class DriversLicencseImageBuilder
    {
        public static MemoryStream Build()
        {
            var image = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(100, 100);
            var memoryStream = new MemoryStream();
            image.SaveAsBmp(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
