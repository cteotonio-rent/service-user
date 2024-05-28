namespace rent.domain.Entities
{
    public class Motorcycle : EntityBase
    {
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public Guid UserUniqueIdentifier { get; set; }
    }
}
