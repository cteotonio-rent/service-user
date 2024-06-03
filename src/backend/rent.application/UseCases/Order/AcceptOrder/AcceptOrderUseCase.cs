
using MongoDB.Bson;
using rent.communication.Requests;
using rent.domain.Entities;
using rent.domain.Enuns;
using rent.domain.Repositories;
using rent.domain.Repositories.NotifyDeliveryPerson;
using rent.domain.Repositories.Order;
using rent.domain.Repositories.User;
using rent.domain.Services.LoggedUser;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Order.AcceptOrder
{
    public class AcceptOrderUseCase : IAcceptOrderUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IOrderUpdateOnlyRepository _orderUpdateOnlyRepository;
        private readonly INotifyDeliveryPersonReadOnlyRepository _notifyDeliveryPersonReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AcceptOrderUseCase(
            ILoggedUser loggedUser,
            IOrderUpdateOnlyRepository orderUpdateOnlyRepository,
            INotifyDeliveryPersonReadOnlyRepository notifyDeliveryPersonReadOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _orderUpdateOnlyRepository = orderUpdateOnlyRepository;
            _notifyDeliveryPersonReadOnlyRepository = notifyDeliveryPersonReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestAcceptOrderJson request)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.DeliveryPerson }))
                throw new RentUnauthorizedAccessException();

            var user = await _loggedUser.User();

            var order = await _orderUpdateOnlyRepository.GetById(ObjectId.Parse(request.OrderId));

            if (order is null)
                throw new NotFoundException();

            if (order.Status != (int)OrderStatus.Available)
                throw new UserException(ResourceMessagesException.ORDER_NOT_AVAILABLE);

            if (order.DeliveryPersonId is not null)
                throw new UserException(ResourceMessagesException.ORDER_ACCEPTED);

            var notificationExixt = await _notifyDeliveryPersonReadOnlyRepository.ExisitsNotifyDeliveryPersonWithOrderAndUser(ObjectId.Parse(request.OrderId), user._id);

            if (!notificationExixt)
                throw new UserException(ResourceMessagesException.NOTIFICATION_NOT_SEND);

            order.DeliveryPersonId = user._id;
            order.Status = (int)OrderStatus.Accepted;

            _orderUpdateOnlyRepository.Update(order);

            await _unitOfWork.Commit();
        }
    }
}
