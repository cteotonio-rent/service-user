namespace rent.communication.Responses
{
    public class ResponseGetOrderDeliveryPersonJson
    {
        public string Message { get; set; } = string.Empty;
        public List<DeliveryPersonJson> DeliveryPerson { get; set; } = new List<DeliveryPersonJson>();
    }

    public class DeliveryPersonJson
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
