using rent.domain.Services.ReadNewOrderMessage;

namespace rent.application.UseCases.NotifyDeliveryPerson.Register
{
    public class NotifyDeliveryPersonUseCase : INotifyDeliveryPersonUseCase
    {
        private readonly IReadNewOrderMessage _readNewOrderMessage;
        
        public NotifyDeliveryPersonUseCase(IReadNewOrderMessage readNewOrderMessage)
        {
            _readNewOrderMessage = readNewOrderMessage;
        }

        public async Task Execute()
        {
            await _readNewOrderMessage.ReadAndNotifyNewOrder();
        }
    }
}
