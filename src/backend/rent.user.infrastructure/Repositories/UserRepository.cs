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

    }
}
