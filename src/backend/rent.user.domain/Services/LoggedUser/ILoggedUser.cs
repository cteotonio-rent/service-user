using rent.user.domain.Entities;

namespace rent.user.domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
