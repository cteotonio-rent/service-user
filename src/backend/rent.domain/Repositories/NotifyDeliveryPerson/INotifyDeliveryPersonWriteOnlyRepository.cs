
namespace rent.domain.Repositories.NotifyDeliveryPerson
{
    public interface INotifyDeliveryPersonWriteOnlyRepository
    {
        Task Add(Entities.DeliveryPersonNotification deliveryPersonNotification);
    }
}
