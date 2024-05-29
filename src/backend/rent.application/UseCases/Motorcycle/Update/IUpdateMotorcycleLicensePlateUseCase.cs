using MongoDB.Bson;
using rent.communication.Requests;

namespace rent.application.UseCases.Motorcycle.Update
{
    public interface IUpdateMotorcycleLicensePlateUseCase
    {
        Task Execute(ObjectId id, RequestUpdateMotorcycleLicensePlateJson request);
    }
}
