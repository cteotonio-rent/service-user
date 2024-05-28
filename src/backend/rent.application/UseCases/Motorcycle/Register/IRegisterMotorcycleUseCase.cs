using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.Motorcycle.Register
{
    public interface IRegisterMotorcycleUseCase
    {
        public Task<ResponseRegisteredMotorcycleJson> Execute(RequestRegisterMotorcycleJson request);
    }
}
