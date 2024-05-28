using Microsoft.EntityFrameworkCore;
using rent.domain.Entities;
using rent.domain.Repositories.Motorcycle;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleReadOnlyRepository, IMotorcycleWriteOnlyRepository
    {
        private readonly UserDbContext _dbContext;
        public MotorcycleRepository(UserDbContext context) => _dbContext = context;

        public async Task Add(Motorcycle motorcycle) => await _dbContext.Motorcycles.AddAsync(motorcycle);

        public async Task<bool> ExistActiveMotorcycleWithLicensePlate(string licensePlate) => await _dbContext.Motorcycles.AnyAsync(m => m.LicensePlate.Equals(licensePlate) && m.Active);

        public async Task<Motorcycle?> GetMotorcycleByLicensePlate(string licensePlate) => await _dbContext.Motorcycles.FirstOrDefaultAsync(m => m.LicensePlate.Equals(licensePlate) && m.Active);
    }

}
