using MongoDB.Bson;

namespace rent.domain.Entities
{
    public class DeliveryPersonNotification: EntityBase
    {
        public string Message { get; set; } = string.Empty;
        public List<DeliveryPerson> User { get; set; } = new List<DeliveryPerson>();
        public ObjectId OrderId { get; set; }
    }

    public class DeliveryPerson
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
