using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUserJson request);
    }
}
