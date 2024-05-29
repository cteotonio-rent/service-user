using rent.domain.Repositories;
using rent.domain.Repositories.User;
using rent.domain.Services.LoggedUser;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.User.Update
{
    public class UpdateUserImageUseCase: IUpdateUserImageUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserImageUseCase(
            ILoggedUser loggedUser,
            IUserUpdateOnlyRepository userUpdateOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(MemoryStream memoryStream, string extension)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(memoryStream, extension);

            var user = await _userUpdateOnlyRepository.GetById(loggedUser._id);

            user.DriversLicenseImage = loggedUser._id.ToString() + $".{extension}";

            _userUpdateOnlyRepository.Update(user);
            await _unitOfWork.Commit();
        }

        private async Task Validate(MemoryStream memoryStream, string extension)
        {
            if (extension != ".png" && extension != ".bmp")
                throw new InvalidFileTypeException();

            var permittedMimeTypes = new[] { "image/png", "image/bmp" };

            var fileType = GetFileType(memoryStream);

            if (!permittedMimeTypes.Contains(fileType))
            {
                throw new InvalidFileTypeException();
            }
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
