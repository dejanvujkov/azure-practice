using System;
using System.Threading.Tasks;
using Common.Models;
using SBSender.Services;

namespace SBSender
{
    class Program
    {
        private const string queueName = "";

        static async Task Main(string[] args)
        {
            var person = new PersonModel() {
                FirstName = "Ana",
                LastName = "Doe"
            };

            var queueService = new QueueService();
            await queueService.SendMessageAsync<PersonModel>(person, queueName);
            System.Console.WriteLine($"Message sent to queue {queueName}!");
        }
    }
}
