using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.User.Update;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace UseCases.Test.User.Update
{
    public class UpdateUserUseCaseTest
    {

        [Fact]
        public async Task Sucess()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();

            user.Name.Should().Be(request.Name);
            user.Email.Should().Be(request.Email);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            var usecase = CreateUseCase(user, request.Email);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            user.Name.Should().NotBe(request.Name);
            user.Email.Should().NotBe(request.Email);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;
            var usecase = CreateUseCase(user);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));

            user.Name.Should().NotBe(request.Name);
            user.Email.Should().NotBe(request.Email);
        }

        private static UpdateUserUseCase CreateUseCase(rent.domain.Entities.User user, string? email = null)
        {
            var loggedUser = new LoggedUserBuilder().IsAuthorized(user).Build(user);
            var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();

            if (!string.IsNullOrEmpty(email))
                userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new UpdateUserUseCase(loggedUser,
                userUpdateOnlyRepository,
                userReadOnlyRepositoryBuilder.Build(),
                unitOfWork);
        }
    }
}
