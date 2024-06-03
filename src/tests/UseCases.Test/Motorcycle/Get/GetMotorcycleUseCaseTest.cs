using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using FluentAssertions;
using rent.application.UseCases.Motorcycle.Get;

namespace UseCases.Test.Motorcycle.Get
{
    public class GetMotorcycleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var _) = UserBuilder.Build();
            user.UserType = rent.domain.Enuns.UserType.Admin;
            var motorcycle = MotorcycleBuilder.Build();

            var useCase = CreateUseCase(motorcycle, user);
            var response = await useCase.Execute(motorcycle.LicensePlate);

            response.Should().NotBeNull();
            response.LicensePlate.Should().Be(motorcycle.LicensePlate);
            response.Model.Should().Be(motorcycle.Model);
            response.Year.Should().Be(motorcycle.Year);
        }

        [Fact]
        public async Task Success_No_Result()
        {
            (var user, var _) = UserBuilder.Build();
            user.UserType = rent.domain.Enuns.UserType.Admin;
            var motorcycle = MotorcycleBuilder.Build();

            var useCase = CreateUseCase(motorcycle, user);
            var response = await useCase.Execute("");

            response.Should().BeNull();
        }

        private static GetMotorcycleUseCase CreateUseCase(rent.domain.Entities.Motorcycle motorcycle, rent.domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = new LoggedUserBuilder().IsAuthorized(user).Build(user);
            var motorcycleReadOnlyRepositoryBuilder = new MotorcycleReadOnlyRepositoryBuilder();

            if (motorcycle is not null)
                motorcycleReadOnlyRepositoryBuilder.GetMotorcycleByLicensePlate(motorcycle);

            return new GetMotorcycleUseCase(
                motorcycleReadOnlyRepositoryBuilder.Build(),
                loggedUser,
                mapper
                );
        }
    }
}
