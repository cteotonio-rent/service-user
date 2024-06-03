using AutoMapper;
using MongoDB.Bson;
using rent.communication.Responses;
using rent.domain.Enuns;
using rent.domain.Repositories.NotifyDeliveryPerson;
using rent.domain.Services.LoggedUser;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Order.GetOrderDeliverMan
{
    public class GetOrderDeliveryPersonUseCase : IGetOrderDeliveryPersonUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly INotifyDeliveryPersonReadOnlyRepository _notifyDeliveryPersonReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetOrderDeliveryPersonUseCase(
            INotifyDeliveryPersonReadOnlyRepository notifyDeliveryPersonReadOnlyRepository,
            ILoggedUser loggedUser,
            IMapper mapper)
        {
            _notifyDeliveryPersonReadOnlyRepository = notifyDeliveryPersonReadOnlyRepository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseGetOrderDeliveryPersonJson> Execute(string orderId)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.Admin }))
                throw new RentUnauthorizedAccessException();

            var notification = await _notifyDeliveryPersonReadOnlyRepository.GetNotifyDeliveryPersonWithOrder(ObjectId.Parse( orderId));

            var response = _mapper.Map<ResponseGetOrderDeliveryPersonJson>(notification);

            return response;
        }
    }
}
