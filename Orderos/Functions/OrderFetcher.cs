using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orderos.Entities;
using Orderos.Services;

namespace Orderos.Functions
{
    public class OrderFetcher
    {
        private readonly ILogger<OrderFetcher> _logger;
		private readonly CourseService _courseService;

		public OrderFetcher(ILogger<OrderFetcher> logger, CourseService courseService)
        {
            _logger = logger;
			_courseService = courseService;
		}

        [Function("GetAllSavedCourses")]	
		public async Task<IActionResult> GetAllSavedCourses( 
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders")] HttpRequest req)
		{
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			var serviceResponse = await _courseService.GetAllSavedCourses();

			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			return new OkObjectResult(serviceResponse.Data);
		}

		[Function("GetSavedCourse")]
		public async Task<IActionResult> GetSavedCourse(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{id}")] HttpRequest req, int id)
		{
			var serviceResponse = await _courseService.GetSavedCourseById(id);
			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new NotFoundResult();
			}

			return new OkObjectResult(serviceResponse.Data);
		}



		[Function("SaveCourse")]
		public async Task<IActionResult> SaveCourse(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
		{
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			var savedCourse = JsonConvert.DeserializeObject<SavedCoursesEntity>(requestBody);

			var serviceResponse = await _courseService.SaveCourse(savedCourse);

			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			return new OkObjectResult(serviceResponse.Data);
		}

		[Function("DeleteSavedCourse")]
		public async Task<IActionResult> DeleteSavedCourse(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "orders/{id}")] HttpRequest req, int id)
		{
			var serviceResponse = await _courseService.GetSavedCourseById(id);
			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new NotFoundResult();
			}

			var deleteResponse = await _courseService.DeleteSavedCourse(serviceResponse.Data);

			if (!deleteResponse.Success)
			{
				_logger.LogError(deleteResponse.Message);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			return new OkResult();
		}
	}
}


