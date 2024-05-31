using rent.domain.Entities;
using rent.domain.Enuns;

namespace rent.domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
        public Task<bool> IsAuthorized(List<UserType> userTypeList);
    }
}
