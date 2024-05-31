using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using rent.domain.Entities;
using rent.domain.Repositories.Rental;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class RentalRepository : IRentalReadOnlyRepository, IRentalWriteOnlyRepository
    {
        private readonly UserDbContext _dbContext;
        public RentalRepository(UserDbContext context) => _dbContext = context;
        public async Task Add(Rental rental) => await _dbContext.Rentals.AddAsync(rental);
        public async Task<bool> ExistsRentalWithMotorcycle(ObjectId motorcycleId) =>
            await _dbContext.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId);
    }
}
