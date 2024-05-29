using rent.communication.Responses;

namespace rent.application.UseCases.Order.GetOrderDeliverMan
{
    public interface IGetOrderDeliveryPersonUseCase
    {
        Task<ResponseGetOrderDeliveryPersonJson> Execute(string orderId);
    }
}
