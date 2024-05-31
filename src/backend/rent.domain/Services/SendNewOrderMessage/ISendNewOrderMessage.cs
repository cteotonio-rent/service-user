
namespace rent.domain.Services.SendNewOrderMessage
{
    public interface ISendNewOrderMessage
    {
        Task SendMessage(string message);
    }
}
