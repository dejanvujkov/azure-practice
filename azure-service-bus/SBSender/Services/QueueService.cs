using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;

namespace SBSender.Services
{

    public class QueueService : IQueueService
    {
        private const string connectionString = "";
        public QueueService()
        {
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            var queueClient = new QueueClient(connectionString, queueName);
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.SendAsync(message);
        }
    }
}