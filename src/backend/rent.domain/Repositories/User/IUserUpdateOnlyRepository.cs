using MongoDB.Bson;

namespace rent.domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        Task<rent.domain.Entities.User> GetById(ObjectId id);
        void Update(rent.domain.Entities.User user);
    }
}
