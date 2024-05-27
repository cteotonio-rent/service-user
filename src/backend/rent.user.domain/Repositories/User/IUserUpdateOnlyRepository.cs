using MongoDB.Bson;

namespace rent.user.domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        Task<rent.user.domain.Entities.User> GetById(ObjectId id);
        void Update(rent.user.domain.Entities.User user);
    }
}
