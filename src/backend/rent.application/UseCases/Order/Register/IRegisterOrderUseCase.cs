using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.Order.Register
{
    public interface IRegisterOrderUseCase
    {
        Task<ResponseRegisteredOrderJson> Execute(RequestRegisterOrderJson request);
    }
}
