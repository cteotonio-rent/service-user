using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.Motorcycle.Register;
using rent.application.UseCases.Motorcycle.Update;
using rent.exceptions.ExceptionsBase;
using rent.exceptions;
using rent.domain.Entities;

namespace UseCases.Test.Motorcycle.Update
{
    public class UpdateMotorcycleLicensePlateUseCaseTest
    {


        [Fact]
        public async Task Sucess()
        {
            (var user, _) = UserBuilder.Build();
            var motorcycle = MotorcycleBuilder.Build();
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();

            var useCase = CreateUseCase(user, motorcycle);
            Func<Task> act = async () => await useCase.Execute(motorcycle._id, request);

            await act.Should().NotThrowAsync();

            motorcycle.LicensePlate.Should().Be(request.LicensePlate);
        }

        [Fact]
        public async Task Error_License_Plate_Already_Registered()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();
            var motorcycle = MotorcycleBuilder.Build();
            request.LicensePlate = motorcycle.LicensePlate;
            var useCase = CreateUseCase(user, motorcycle, request.LicensePlate);

            Func<Task> act = async () => await useCase.Execute(MongoDB.Bson.ObjectId.GenerateNewId(), request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.LICENSE_PLATE_ALREADY_REGISTERED));
        }


        private static UpdateMotorcycleLicensePlateUseCase CreateUseCase(
            rent.domain.Entities.User user,
            rent.domain.Entities.Motorcycle motorcycle,
            string? licencePlate = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var mapper = MapperBuilder.Build();
            var motorcycleUpdateOnlyRepository = new MotorcycleUpdateOnlyRepositoryBuilder().GetById(motorcycle).Build();
            var motorcycleReadOnlyRepositoryBuilder = new MotorcycleReadOnlyRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();

            if (!string.IsNullOrEmpty(licencePlate))
                motorcycleReadOnlyRepositoryBuilder.ExistActiveMotorcycleWithLicensePlate(licencePlate);
             

            if (motorcycle is not null)
                motorcycleReadOnlyRepositoryBuilder.GetMotorcycleByLicensePlate(motorcycle);

            return new UpdateMotorcycleLicensePlateUseCase(
                loggedUser,
                motorcycleUpdateOnlyRepository,
                motorcycleReadOnlyRepositoryBuilder.Build(),
                unitOfWork);
        }
    }
}
