using rent.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.infrastructure.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly UserDbContext _dbContext;

        public UnitOfWork(UserDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();

    }
}
