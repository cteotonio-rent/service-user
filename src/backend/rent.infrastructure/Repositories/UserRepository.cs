using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using rent.domain.Entities;
using rent.domain.Enuns;
using rent.domain.Repositories.User;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext context) => _dbContext = context;

        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

        public void Update(User user) =>  _dbContext.Users.Update(user);

        public async Task<User> GetById(ObjectId id) => await _dbContext.Users.FirstAsync(user => user._id == id);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(u => u.Email == email && u.Active);

        public async Task<bool> ExistActiveUserWithNRLE(string NRLE) => await _dbContext.Users.AnyAsync(u => u.NRLE == NRLE && u.Active);

        public async Task<bool> ExistActiveUserWithDriversLicense(string driversLicense) => await _dbContext.Users.AnyAsync(u => u.DriversLicense == driversLicense && u.Active);

        public async Task<bool?> ExistsActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(u => u.UserUniqueIdentifier.Equals(userIdentifier) && u.Active);
        
        public async Task<User?> GetByUserIdentifier(Guid userIdentifier) => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserUniqueIdentifier.Equals(userIdentifier) && u.Active);
        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Active);
        }

        public IList<User?> GetDeliveryPersonAvaiableAndFree() => [.. _dbContext.Users.AsNoTracking().Where(u => u.Active && u.UserType == UserType.DeliveryPerson
         && _dbContext.Rentals.AsNoTracking().Where(r => r.UserId == u._id && r.Active).Count() > 0
        && _dbContext.Orders.AsNoTracking().Where(o => o.Active && o.DeliveryPersonId == u._id && o.Status != (int)OrderStatus.Accepted).Count() == 0
        )];
    }
}
