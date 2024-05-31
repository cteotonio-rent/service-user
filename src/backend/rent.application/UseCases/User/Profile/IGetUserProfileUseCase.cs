using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
