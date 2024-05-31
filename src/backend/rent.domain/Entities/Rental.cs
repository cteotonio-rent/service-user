using MongoDB.Bson;

namespace rent.domain.Entities
{
    public class Rental: EntityBase
    {
        public ObjectId UserId { get; set; } = ObjectId.Empty;
        public ObjectId MotorcycleId { get; set; } = ObjectId.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public DateTime? RealEndDate { get; set; } = null;
        public decimal? TotalPrice { get; set; } = 0;
        public decimal EstimatedPrice { get; set; } = 0;
        public decimal DailyFineNotApplied { get; set; } = 0;
        public decimal AdditionalDailyFine { get; set; } = 0;
        public decimal DurationInDays { get; set; } = 0;
        public ObjectId RentalPlanId { get; set; } = ObjectId.Empty;
    }
}
