using rent.communication.Requests;

namespace rent.application.UseCases.Order.DeliverOrder
{
    public interface IDeliverOrderUseCase
    {
        Task Execute(RequestDeliverOrderJson request);
    }
}
