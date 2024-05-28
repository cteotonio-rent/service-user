using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
