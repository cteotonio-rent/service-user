using Moq;
using rent.domain.Entities;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
    public class MotorcycleReadOnlyRepositoryBuilder
    {
        private readonly Mock<IMotorcycleReadOnlyRepository> _repository;

        public MotorcycleReadOnlyRepositoryBuilder() => _repository = new Mock<IMotorcycleReadOnlyRepository>();

        public void ExistActiveMotorcycleWithLicensePlate(string licensePlate)
        {
            _repository.Setup(repo => repo.ExistActiveMotorcycleWithLicensePlate(licensePlate)).ReturnsAsync(true);
        }

        public void GetMotorcycleByLicensePlate(Motorcycle motorcycle)
        {
            _repository.Setup(repo => repo.GetMotorcycleByLicensePlate(motorcycle.LicensePlate)).ReturnsAsync(motorcycle);
        }

        public IMotorcycleReadOnlyRepository Build() => _repository.Object;
    }
}
