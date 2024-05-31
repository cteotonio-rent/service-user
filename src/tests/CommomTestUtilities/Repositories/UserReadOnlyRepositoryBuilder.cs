using Moq;
using rent.domain.Entities;
using rent.domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

        public void ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(repo => repo.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }

        public void ExistActiveUserWithNRLE(string NRLE)
        {
            _repository.Setup(repo => repo.ExistActiveUserWithNRLE(NRLE)).ReturnsAsync(true);
        }

        public void ExistActiveUserWithDriversLicense(string driversLicense)
        {
            _repository.Setup(repo => repo.ExistActiveUserWithDriversLicense(driversLicense)).ReturnsAsync(true);
        }

        public void GetByEmailAndPassword(User user)
        {
            _repository.Setup(repo => repo.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
        }

        public IUserReadOnlyRepository Build() => _repository.Object;
        
    }
}
