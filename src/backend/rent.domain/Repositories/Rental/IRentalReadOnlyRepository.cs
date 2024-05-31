using MongoDB.Bson;

namespace rent.domain.Repositories.Rental
{
    public interface IRentalReadOnlyRepository
    {
        Task<bool> ExistsRentalWithMotorcycle(ObjectId motorcycleId);
    }
}
