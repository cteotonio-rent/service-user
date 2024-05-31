using MongoDB.Bson;

namespace rent.domain.Repositories.Motorcycle
{
    public interface IMotorcycleUpdateOnlyRepository
    {
        Task<Entities.Motorcycle> GetById(ObjectId id);
        void UpdateLicensePlate(Entities.Motorcycle motorcycle);
        void Update(Entities.Motorcycle motorcycle);
    }
}
