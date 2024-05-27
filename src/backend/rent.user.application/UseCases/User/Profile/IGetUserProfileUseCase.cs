using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
