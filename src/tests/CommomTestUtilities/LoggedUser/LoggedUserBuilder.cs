using CommomTestUtilities.Repositories;
using Moq;
using rent.domain.Entities;
using rent.domain.Enuns;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Services.LoggedUser;

namespace CommomTestUtilities.LoggedUser
{
    public class LoggedUserBuilder
    {
        private readonly Mock<ILoggedUser> _loggedUser;

        public LoggedUserBuilder() => _loggedUser = new Mock<ILoggedUser>();

        public ILoggedUser Build(User user)
        {
           _loggedUser.Setup(x => x.User()).ReturnsAsync(user);

            return _loggedUser.Object;
        }

        public LoggedUserBuilder IsAuthorized(User user)
        {
            var list = new List<UserType>();
            list.Add((UserType)user.UserType!);

            _loggedUser.Setup(r => r.IsAuthorized(list)).ReturnsAsync(true);
            return this;
        }
    }
}
