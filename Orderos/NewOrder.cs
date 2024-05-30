using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NewOrder
{
    public class NewOrder
    {
        private readonly ILogger<NewOrder> _logger;

        public NewOrder(ILogger<NewOrder> logger)
        {
            _logger = logger;
        }

        [Function("NewOrder")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
