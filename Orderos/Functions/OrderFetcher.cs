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

        [Function("GetAllCourses")]	
		public async Task<IActionResult> GetAllCourses( 
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
		{
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			var serviceResponse = await _courseService.GetSavedCourses();

			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			return new OkObjectResult(serviceResponse.Data);
		}

		[Function("GetCourse")]
		public async Task<IActionResult> GetCourse(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "courses/{id}")] HttpRequest req, int id)
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

		[Function("DeleteCourse")]
		public async Task<IActionResult> DeleteCourse(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "courses/{id}")] HttpRequest req, int id)
		{
			var serviceResponse = await _courseService.GetSavedCourseById(id);
			if (!serviceResponse.Success)
			{
				_logger.LogError(serviceResponse.Message);
				return new NotFoundResult();
			}

			var deleteResponse = await _courseService.DeleteCourse(serviceResponse.Data);

			if (!deleteResponse.Success)
			{
				_logger.LogError(deleteResponse.Message);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			return new OkResult();
		}
	}
}


