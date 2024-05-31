using Amazon.SQS;
using Amazon.SQS.Model;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using rent.domain.Repositories;
using rent.domain.Repositories.NotifyDeliveryPerson;
using rent.domain.Repositories.User;
using rent.domain.Services.ReadNewOrderMessage;

namespace rent.infrastructure.Services.ReadNewOrderMessage
{
    public class ReadNewOrderMessage : IReadNewOrderMessage
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;
        private readonly INotifyDeliveryPersonWriteOnlyRepository _notifyDeliveryPersonWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public ReadNewOrderMessage(IAmazonSQS sqsClient,
            string queueUrl,
            INotifyDeliveryPersonWriteOnlyRepository notifyDeliveryPersonWriteOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _sqsClient = sqsClient;
            _queueUrl = queueUrl;
            _notifyDeliveryPersonWriteOnlyRepository = notifyDeliveryPersonWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task ReadAndNotifyNewOrder()
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 1,
                WaitTimeSeconds = 20
            };

            while (true)
            {
                var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);

                if (receiveMessageResponse.Messages.Count == 0)
                {
                    Console.WriteLine("No messages received. Waiting for new messages...");
                    continue;
                }

                var message = receiveMessageResponse.Messages[0];
                var messageBody = message.Body;

                var deliveryPersonList = _userReadOnlyRepository.GetDeliveryPersonAvaiableAndFree();

                if (deliveryPersonList == null) continue;

                var notifyDeliveryPerson = new rent.domain.Entities.DeliveryPersonNotification();

                notifyDeliveryPerson.User.AddRange(
                    deliveryPersonList.Select(deliveryPerson => new rent.domain.Entities.DeliveryPerson
                    {
                        Id = deliveryPerson!._id,
                        Name = deliveryPerson.Name,
                        Email = deliveryPerson.Email
                    }).ToList()
                );

                notifyDeliveryPerson.Message = messageBody;
                notifyDeliveryPerson.OrderId = ObjectId.Parse(JObject.Parse(messageBody)["OrderId"]!.ToString());

                await _notifyDeliveryPersonWriteOnlyRepository.Add(notifyDeliveryPerson);

                await _unitOfWork.Commit();

                var deleteMessageRequest = new DeleteMessageRequest
                {
                    QueueUrl = _queueUrl,
                    ReceiptHandle = message.ReceiptHandle
                };

                await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
            }
        }
    }
}
