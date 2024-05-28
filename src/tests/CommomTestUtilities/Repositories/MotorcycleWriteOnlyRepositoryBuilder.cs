using Moq;
using rent.domain.Repositories.Motorcycle;

namespace CommomTestUtilities.Repositories
{
    public class MotorcycleWriteOnlyRepositoryBuilder
    {
        public static IMotorcycleWriteOnlyRepository Build()
        {
            var mock = new Mock<IMotorcycleWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
