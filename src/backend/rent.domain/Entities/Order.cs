using MongoDB.Bson;

namespace rent.domain.Entities
{
    public class Order: EntityBase
    {
        public decimal Price { get; set; } = 0;
        public int Status { get; set; } = 0;
        public ObjectId? UserId { get; set; } = null;
    }
}
