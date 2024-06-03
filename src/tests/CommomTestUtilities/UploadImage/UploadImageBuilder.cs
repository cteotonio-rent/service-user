using Moq;
using rent.domain.Entities;
using rent.domain.Services.LoggedUser;
using rent.domain.Services.UploadImage;

namespace CommomTestUtilities.UploadImage
{
    public class UploadImageBuilder
    {
        public static IUploadImage Build()
        {
            var mock = new Mock<IUploadImage>();

            return mock.Object;
        }
    }
}
