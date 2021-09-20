using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;
using Common.Models;

namespace SBReciever
{
    class Program
    {
        const string connectionString = "";
        const string queueName = "";
        static IQueueClient queueClient;
        static async Task Main(string[] args)
        {

            queueClient = new QueueClient(connectionString, queueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
            
            Console.ReadLine();

            await queueClient.CloseAsync();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            var person = JsonSerializer.Deserialize<PersonModel>(jsonString);
            System.Console.WriteLine($"Person received: {person.FirstName} {person.LastName}"); // do something with this.
            
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            System.Console.WriteLine($"Message handler exception: {arg.Exception}");
            return Task.CompletedTask;
        }
    }
}
