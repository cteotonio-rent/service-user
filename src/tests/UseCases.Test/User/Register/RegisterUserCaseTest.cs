using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using rent.application.UseCases.User.Register;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace UseCases.Test.User.Register
{
    public class RegisterUserCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Tokens.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var usecase = CreateUseCase(request.Email);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var usecase = CreateUseCase();

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_NRLE_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var usecase = CreateUseCase(NRLE: request.NRLE);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NRLE_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_DriversLicense_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var usecase = CreateUseCase(driversLicense: request.DriversLicense);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.DRIVERS_LICENSE_ALREADY_REGISTERED));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null, string? NRLE = null, string? driversLicense = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

            if (string.IsNullOrEmpty(email) == false)
                userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);

            if (string.IsNullOrEmpty(NRLE) == false)
                userReadOnlyRepositoryBuilder.ExistActiveUserWithNRLE(NRLE);

            if (string.IsNullOrEmpty(driversLicense) == false)
                userReadOnlyRepositoryBuilder.ExistActiveUserWithDriversLicense(driversLicense);

            return new RegisterUserUseCase(userWriteOnlyRepository, 
                userReadOnlyRepositoryBuilder.Build(), 
                unitOfWork, mapper, passwordEncripter, accessTokenGenerator);
        }
    }
}
