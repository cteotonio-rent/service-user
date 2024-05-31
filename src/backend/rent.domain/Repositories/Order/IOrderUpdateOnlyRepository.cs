using MongoDB.Bson;

namespace rent.domain.Repositories.Order
{
    public interface IOrderUpdateOnlyRepository
    {
        void Update(Entities.Order order);
        Task<Entities.Order?> GetById(ObjectId id);
    }
}
