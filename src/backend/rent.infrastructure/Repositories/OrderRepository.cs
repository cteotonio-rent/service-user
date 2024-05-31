using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using rent.domain.Entities;
using rent.domain.Repositories.Order;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class OrderRepository : BaseRepository, IOrderWriteOnlyRepository, IOrderUpdateOnlyRepository
    {
        public OrderRepository(UserDbContext context) : base(context)
        {
        }

        public async Task Add(Order order) => await _dbContext.Orders.AddAsync(order);

        public async Task<Order?> GetById(ObjectId id) => await _dbContext.Orders.FirstAsync(order => order._id == id);

        public void Update(Order order) => _dbContext.Orders.Update(order);
    }
}
