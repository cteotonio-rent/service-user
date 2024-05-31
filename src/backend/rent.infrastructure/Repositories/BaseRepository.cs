using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly UserDbContext _dbContext;
        public BaseRepository(UserDbContext context) => _dbContext = context;
    }
}
