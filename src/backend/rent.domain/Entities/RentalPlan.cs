namespace rent.domain.Entities
{
    public class RentalPlan: EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int DurationInDays { get; set; } = 0;
        public decimal DailyFineNotApplied { get; set; } = 0;
        public decimal AdditionalDailyFine { get; set; } = 0;
    }
}
