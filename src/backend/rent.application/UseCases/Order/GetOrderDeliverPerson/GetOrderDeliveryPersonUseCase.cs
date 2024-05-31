using AutoMapper;
using MongoDB.Bson;
using rent.communication.Responses;
using rent.domain.Repositories.NotifyDeliveryPerson;

namespace rent.application.UseCases.Order.GetOrderDeliverMan
{
    public class GetOrderDeliveryPersonUseCase : IGetOrderDeliveryPersonUseCase
    {
        private readonly INotifyDeliveryPersonReadOnlyRepository _notifyDeliveryPersonReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetOrderDeliveryPersonUseCase(
            INotifyDeliveryPersonReadOnlyRepository notifyDeliveryPersonReadOnlyRepository, 
            IMapper mapper)
        {
            _notifyDeliveryPersonReadOnlyRepository = notifyDeliveryPersonReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseGetOrderDeliveryPersonJson> Execute(string orderId)
        {
            var notification = await _notifyDeliveryPersonReadOnlyRepository.GetNotifyDeliveryPersonWithOrder(ObjectId.Parse( orderId));

            var response = _mapper.Map<ResponseGetOrderDeliveryPersonJson>(notification);

            return response;
        }
    }
}
