using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using rent.domain.Entities;
using rent.domain.Repositories.NotifyDeliveryPerson;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class NotifyDeliveryPersonRepository : BaseRepository, INotifyDeliveryPersonReadOnlyRepository, INotifyDeliveryPersonWriteOnlyRepository
    {
        public NotifyDeliveryPersonRepository(UserDbContext context) : base(context) { }

        public async Task Add(DeliveryPersonNotification deliveryPersonNotification) => await _dbContext.DeliveryPersonNotifications.AddAsync(deliveryPersonNotification);
 
        public async Task<DeliveryPersonNotification?> GetNotifyDeliveryPersonWithOrder(ObjectId orderId) => await _dbContext.DeliveryPersonNotifications.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == orderId);

        public async Task<bool> ExisitsNotifyDeliveryPersonWithOrderAndUser(ObjectId orderId, ObjectId userId) => await _dbContext.DeliveryPersonNotifications.AsNoTracking().AnyAsync(x => x.OrderId == orderId && x.User.FirstOrDefault(u => u.Id == userId) != null);

    }
}
