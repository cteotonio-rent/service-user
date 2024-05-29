using rent.communication.Requests;

namespace rent.application.UseCases.Order.AcceptOrder
{
    public interface IAcceptOrderUseCase
    {
        Task Execute(RequestAcceptOrderJson request);
    }
}
