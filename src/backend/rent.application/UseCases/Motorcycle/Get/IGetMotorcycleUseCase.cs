using rent.communication.Responses;

namespace rent.application.UseCases.Motorcycle.Get
{
    public interface IGetMotorcycleUseCase
    {
        Task<ResponseGetMotocycle> Execute(string licensePlate);
    }
}
