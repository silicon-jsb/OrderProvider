using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Orderos.Services;

namespace Orderos.Functions
{
    public class OrderFetcher
    {
        private readonly ILogger<OrderFetcher> _logger;
		private readonly CourseService _courseService;

		public OrderFetcher(ILogger<OrderFetcher> logger)
        {
            _logger = logger;
			_courseService = courseService;
		}

        [Function("OrderFetcher")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.") ;
			var courses = _courseService.GetSavedCourses();
			return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
