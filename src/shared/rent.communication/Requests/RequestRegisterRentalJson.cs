namespace rent.communication.Requests
{
    public class RequestRegisterRentalJson
    {
        public DateTime RealEndDate { get; set; } = DateTime.MinValue;
        public int RentalPlanDays { get; set; } = 0;

    }
}
