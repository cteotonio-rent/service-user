using Microsoft.EntityFrameworkCore;
using rent.user.domain.Entities;
using rent.user.domain.Repositories.User;
using rent.user.infrastructure.DataAccess;
using System.ComponentModel;

namespace rent.user.infrastructure.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext context) => _dbContext = context;

        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(u => u.Email == email && u.Active);

        public async Task<bool?> ExistsActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(u => u.UserUniqueIdentifier.Equals(userIdentifier) && u.Active);
        
        public async Task<User?> GetByUserIdentifier(Guid userIdentifier) => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserUniqueIdentifier.Equals(userIdentifier) && u.Active);
        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Active);
        }
    }
}
