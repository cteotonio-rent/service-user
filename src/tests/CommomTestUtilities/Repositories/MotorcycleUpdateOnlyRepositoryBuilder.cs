using Moq;
using rent.domain.Entities;
using rent.domain.Repositories.Motorcycle;

namespace CommomTestUtilities.Repositories
{
    public class MotorcycleUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IMotorcycleUpdateOnlyRepository> _repository;

        public MotorcycleUpdateOnlyRepositoryBuilder() => _repository = new Mock<IMotorcycleUpdateOnlyRepository>();
        public IMotorcycleUpdateOnlyRepository Build() => _repository.Object;

        public MotorcycleUpdateOnlyRepositoryBuilder GetById(Motorcycle motorcycle)
        {
            _repository.Setup(r => r.GetById(motorcycle._id)).ReturnsAsync(motorcycle);
            return this;
        }
    }
}
