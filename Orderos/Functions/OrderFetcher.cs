using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Orderos.Functions
{
    public class OrderFetcher
    {
        private readonly ILogger<OrderFetcher> _logger;

        public OrderFetcher(ILogger<OrderFetcher> logger)
        {
            _logger = logger;
        }

        [Function("OrderFetcher")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.") ;
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
