using Newtonsoft.Json;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Enuns;
using rent.domain.Repositories;
using rent.domain.Repositories.Order;
using rent.domain.Services.LoggedUser;
using rent.domain.Services.SendNewOrderMessage;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Order.Register
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly ISendNewOrderMessage _sendNewOrderMessage;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterOrderUseCase(
            ILoggedUser loggedUser, 
            IOrderWriteOnlyRepository orderWriteOnlyRepository, 
            ISendNewOrderMessage sendNewOrderMessage, 
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
            _sendNewOrderMessage = sendNewOrderMessage;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredOrderJson> Execute(RequestRegisterOrderJson request)
        {
            var user = await _loggedUser.User(); 
            await Validate(request);
            var order = new rent.domain.Entities.Order();
            order.Price = request.Price;
            order.Status = (int)OrderStatus.Available;
            order.UserId = user._id;
            await _orderWriteOnlyRepository.Add(order);
            await _unitOfWork.Commit();

            var message = new
            {
                OrderId = order._id.ToString(),
            };

            string messageJsonString = JsonConvert.SerializeObject(message);

            await _sendNewOrderMessage.SendMessage(messageJsonString);
            return new ResponseRegisteredOrderJson
            {
                Id = order._id.ToString(),
            };
        }

        private static async Task Validate(RequestRegisterOrderJson request)
        {
            var validator = new RegisterOrderValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
