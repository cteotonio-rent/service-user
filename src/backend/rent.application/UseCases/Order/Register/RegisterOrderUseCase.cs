using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.Order.Register
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        public Task<ResponseRegisteredOrderJson> Execute(RequestRegisterOrderJson request)
        {
            throw new NotImplementedException();
        }
    }
}
