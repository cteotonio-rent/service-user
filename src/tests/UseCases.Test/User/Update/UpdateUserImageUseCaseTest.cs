using CommomTestUtilities.Entities;
using CommomTestUtilities.Image;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.UploadImage;
using FluentAssertions;
using rent.application.UseCases.User.Update;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;


namespace UseCases.Test.User.Update
{
    public class UpdateUserImageUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            (var user, _) = UserBuilder.Build();
            var request = DriversLicencseImageBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request, ".bmp");

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Erro_Invalid_ImageType()
        {
            (var user, _) = UserBuilder.Build();
            var request = DriversLicencseImageInvalidBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request, ".bmp");

            (await act.Should().ThrowAsync<InvalidFileTypeException>())
               .Where(e => e.Message.Contains(ResourceMessagesException.FILE_TYPE_INVALID));
        }

        private static UpdateUserImageUseCase CreateUseCase(rent.domain.Entities.User user)
        {
            var loggedUser = new LoggedUserBuilder().IsAuthorized(user).Build(user);
            var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var uploadImage = UploadImageBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();


            return new UpdateUserImageUseCase(loggedUser,
                userUpdateOnlyRepository,
                unitOfWork, uploadImage);
        }
    }
}
