using MongoDB.Bson;

namespace rent.domain.Repositories.NotifyDeliveryPerson
{
    public interface INotifyDeliveryPersonReadOnlyRepository
    {
        Task<Entities.DeliveryPersonNotification?> GetNotifyDeliveryPersonWithOrder(ObjectId orderId);
        Task<bool> ExisitsNotifyDeliveryPersonWithOrderAndUser(ObjectId orderId, ObjectId userId);
    }
}
