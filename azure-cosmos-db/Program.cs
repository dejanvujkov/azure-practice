using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace azure_cosmos_db
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Azure Cosmos DB demo");
            var address = new Adress("Becej", 21220);
            var person = new Person(Guid.NewGuid(), "Ana", "Vujkov", address);

            // read appsettings.json for credentials
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config.GetConnectionString("CosmosDb");
            System.Console.WriteLine($"Got connection string: {connectionString}");
            System.Console.WriteLine("Press key for connecting to CosmosDB");

            Console.ReadKey();

            try
            {
                var cosmosClient = new CosmosClient(connectionString);
                var database = await cosmosClient.CreateDatabaseIfNotExistsAsync("PersonDatabase"); // creating database with desired name

                System.Console.WriteLine($"Got database with id: {database.Database.Id}");
                var container = await database.Database.CreateContainerIfNotExistsAsync("PersonContainer", "/adress/city"); // create container with partition key

                var personResponse = await container.Container.CreateItemAsync<Person>(person, new PartitionKey(person.Adress.City));
                System.Console.WriteLine($"New person added! Operation consumed {personResponse.RequestCharge}RU");
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine($"Item in database with id: {person.Id} already exists");
            }
        }
    }
}
