using Moq;
using rent.user.domain.Entities;
using rent.user.domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IUserUpdateOnlyRepository> _repository;

        public UserUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUserUpdateOnlyRepository>();
        public IUserUpdateOnlyRepository Build() => _repository.Object;

        public UserUpdateOnlyRepositoryBuilder GetById(User user)
        {
            _repository.Setup(r => r.GetById(user._id)).ReturnsAsync(user);
            return this;
        }

    }
}
