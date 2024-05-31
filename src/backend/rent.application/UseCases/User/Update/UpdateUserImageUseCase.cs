using rent.domain.Repositories;
using rent.domain.Repositories.User;
using rent.domain.Services.LoggedUser;
using rent.domain.Services.UploadImage;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.User.Update
{
    public class UpdateUserImageUseCase: IUpdateUserImageUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadImage _uploadImage;

        public UpdateUserImageUseCase(
            ILoggedUser loggedUser,
            IUserUpdateOnlyRepository userUpdateOnlyRepository,
            IUnitOfWork unitOfWork,
            IUploadImage uploadImage)
        {
            _loggedUser = loggedUser;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
            _uploadImage = uploadImage;
        }

        public async Task Execute(MemoryStream memoryStream, string extension)
        {
            var loggedUser = await _loggedUser.User();

            var contentType = await Validate(memoryStream, extension);

            await _uploadImage.Execute(memoryStream, loggedUser._id.ToString() + $"{extension}", contentType);

            var user = await _userUpdateOnlyRepository.GetById(loggedUser._id);

            user.DriversLicenseImage = loggedUser._id.ToString() + $"{extension}";

            _userUpdateOnlyRepository.Update(user);
            await _unitOfWork.Commit();
        }

        private async Task<string> Validate(MemoryStream memoryStream, string extension)
        {
            if (extension != ".png" && extension != ".bmp")
                throw new InvalidFileTypeException();

            var permittedMimeTypes = new[] { "image/png", "image/bmp" };

            var fileType = GetFileType(memoryStream);

            if (!permittedMimeTypes.Contains(fileType))
            {
                throw new InvalidFileTypeException();
            }

            return fileType;
        }

        private string GetFileType(Stream stream)
        {
            var imageFormat = SixLabors.ImageSharp.Image.DetectFormat(stream);

            if (imageFormat == SixLabors.ImageSharp.Formats.Png.PngFormat.Instance)
            {
                return "image/png";
            }
            else if (imageFormat == SixLabors.ImageSharp.Formats.Bmp.BmpFormat.Instance)
            {
                return "image/bmp";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
