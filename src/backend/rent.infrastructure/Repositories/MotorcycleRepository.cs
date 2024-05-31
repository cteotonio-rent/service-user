using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using rent.domain.Entities;
using rent.domain.Repositories.Motorcycle;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleReadOnlyRepository, IMotorcycleWriteOnlyRepository, IMotorcycleUpdateOnlyRepository
    {
        private readonly UserDbContext _dbContext;
        public MotorcycleRepository(UserDbContext context) => _dbContext = context;

        public async Task Add(Motorcycle motorcycle) => await _dbContext.Motorcycles.AddAsync(motorcycle);

        public async Task<bool> ExistActiveMotorcycleWithLicensePlate(string licensePlate) => await _dbContext.Motorcycles.AnyAsync(m => m.LicensePlate.Equals(licensePlate) && m.Active);

        public async Task<bool> ExistActiveMotorcycleWithStatus(int statusId) => await _dbContext.Motorcycles.AnyAsync(m => m.MotorcycleStatus.Equals(statusId) && m.Active);

        public async Task<Motorcycle> GetById(ObjectId id) => await _dbContext.Motorcycles.FirstAsync(m => m._id == id);

        public async Task<Motorcycle?> GetFirstActiveMotorcycleByStatus(int statusId) => await _dbContext.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.MotorcycleStatus.Equals(statusId) && m.Active);

        public async Task<Motorcycle?> GetMotorcycleByLicensePlate(string licensePlate) => await _dbContext.Motorcycles.FirstOrDefaultAsync(m => m.LicensePlate.Equals(licensePlate) && m.Active);

        public void UpdateLicensePlate(Motorcycle motorcycle) => Update(motorcycle);

        public void Update(Motorcycle motorcycle) => _dbContext.Motorcycles.Update(motorcycle);
    }

}
