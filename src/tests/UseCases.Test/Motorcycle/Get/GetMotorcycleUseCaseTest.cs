using CommomTestUtilities.Entities;
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
            var motorcycle = MotorcycleBuilder.Build();

            var useCase = CreateUseCase(motorcycle);
            var response = await useCase.Execute(motorcycle.LicensePlate);

            response.Should().NotBeNull();
            response.LicensePlate.Should().Be(motorcycle.LicensePlate);
            response.Model.Should().Be(motorcycle.Model);
            response.Year.Should().Be(motorcycle.Year);
        }

        [Fact]
        public async Task Success_No_Result()
        {
            var motorcycle = MotorcycleBuilder.Build();

            var useCase = CreateUseCase(motorcycle);
            var response = await useCase.Execute("");

            response.Should().BeNull();
        }

        private static GetMotorcycleUseCase CreateUseCase(rent.domain.Entities.Motorcycle motorcycle)
        {
            var mapper = MapperBuilder.Build();
            var motorcycleReadOnlyRepositoryBuilder = new MotorcycleReadOnlyRepositoryBuilder();

            if (motorcycle is not null)
                motorcycleReadOnlyRepositoryBuilder.GetMotorcycleByLicensePlate(motorcycle);

            return new GetMotorcycleUseCase(
                motorcycleReadOnlyRepositoryBuilder.Build(),
                mapper
                );
        }
    }
}
