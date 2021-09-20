using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace azure_funtions
{
    public static class HttpTriggerFunction
    {
        [Function("HttpTriggerFunction")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpTriggerFunction");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(body);

            response.WriteString("Welcome to Azure Functions!\n");
            response.WriteString($"Recieved user in body: {user.ToString()}");

            return response;
        }
    }
}
