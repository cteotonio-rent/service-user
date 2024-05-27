using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUserJson request);
    }
}
