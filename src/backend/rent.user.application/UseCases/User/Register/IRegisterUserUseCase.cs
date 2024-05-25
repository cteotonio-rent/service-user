using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
