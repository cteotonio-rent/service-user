using rent.communication.Requests;
using rent.domain.Repositories.Order;
using rent.domain.Repositories;
using rent.domain.Services.LoggedUser;
using MongoDB.Bson;
using rent.domain.Enuns;
using rent.exceptions.ExceptionsBase;
using rent.exceptions;

namespace rent.application.UseCases.Order.DeliverOrder
{
    public class DeliverOrderUseCase : IDeliverOrderUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IOrderUpdateOnlyRepository _orderUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeliverOrderUseCase(
            ILoggedUser loggedUser,
            IOrderUpdateOnlyRepository orderUpdateOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _orderUpdateOnlyRepository = orderUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(RequestDeliverOrderJson request)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.DeliveryPerson }))
                throw new RentUnauthorizedAccessException();

            var user = await _loggedUser.User();

            var order = await _orderUpdateOnlyRepository.GetById(ObjectId.Parse(request.OrderId));

            if (order is null)
                throw new NotFoundException();

            if (order.Status != (int)OrderStatus.Accepted)
                throw new UserException(ResourceMessagesException.ORDER_NOT_ACCEPTED);

            if (order.DeliveryPersonId != user._id)
                throw new UserException(ResourceMessagesException.ORDER_ACCEPTED_ANOTHER_USER);
                
            order.Status = (int)OrderStatus.Delivered;

            _orderUpdateOnlyRepository.Update(order);

            await _unitOfWork.Commit();
        }
    }
}
