using Amazon.SQS;
using Amazon.SQS.Model;
using rent.domain.Services.SendNewOrderMessage;

namespace rent.infrastructure.Services.SendNewOrderMessage
{
    public class SendNewOrderMessage : ISendNewOrderMessage
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public SendNewOrderMessage(IAmazonSQS sqsClient, string queueUrl)
        {
            _sqsClient = sqsClient;
            _queueUrl = queueUrl;
        }

        public async Task SendMessage(string message)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }

    }
}