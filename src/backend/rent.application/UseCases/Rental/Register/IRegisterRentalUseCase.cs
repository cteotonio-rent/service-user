using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.Rental.Register
{
    public interface IRegisterRentalUseCase
    {
        Task<ResponseRegisteredRentalJson> Execute(RequestRegisterRentalJson request);
    }
}
