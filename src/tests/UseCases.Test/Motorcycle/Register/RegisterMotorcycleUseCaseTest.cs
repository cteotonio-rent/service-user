using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.Motorcycle.Register;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace UseCases.Test.Motorcycle.Register
{
    public class RegisterMotorcycleUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            (var user, _) = UserBuilder.Build();
            user.UserType = rent.domain.Enuns.UserType.Admin;
            var request = RequestRegisterMotorcycleJsonBuilder.Build();

            var useCase = CreateUseCase(user);
            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.LicensePlate.Should().Be(request.LicensePlate);
        }

        [Fact]
        public async Task Error_License_Plate_Already_Registered()
        {
            (var user, _) = UserBuilder.Build();
            user.UserType = rent.domain.Enuns.UserType.Admin;
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            var usecase = CreateUseCase(user, request.LicensePlate);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.LICENSE_PLATE_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_Model_Empty()
        {
            (var user, _) = UserBuilder.Build();
            user.UserType = rent.domain.Enuns.UserType.Admin;
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Model = string.Empty;
            var usecase = CreateUseCase(user);

            Func<Task> act = async () => await usecase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.MODEL_EMPTY));

        }
        private static RegisterMotorcycleUseCase CreateUseCase(rent.domain.Entities.User user, string? licencePlate = null)
        {
            var loggedUser = new LoggedUserBuilder().IsAuthorized(user).Build(user);
            var mapper = MapperBuilder.Build();
            var motorcycleWriteOnlyRepository = MotorcycleWriteOnlyRepositoryBuilder.Build();
            var motorcycleReadOnlyRepositoryBuilder = new MotorcycleReadOnlyRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();

            if (!string.IsNullOrEmpty(licencePlate))
                motorcycleReadOnlyRepositoryBuilder.ExistActiveMotorcycleWithLicensePlate(licencePlate);

            return new RegisterMotorcycleUseCase(
                loggedUser,
                motorcycleReadOnlyRepositoryBuilder.Build(),
                motorcycleWriteOnlyRepository,
                unitOfWork,
                mapper);
        }
    }
}
