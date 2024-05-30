//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Orderos.Data;
//using System.Security.Claims;

//namespace Orderos.Functions
//{
//    public class GetSavedCourses
//        //for frontend
//    {
//        private readonly ILogger<GetSavedCourses> _logger;
//        private readonly DataContext _context;

//        public GetSavedCourses(ILogger<GetSavedCourses> logger, DataContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [Function("GetSavedCourses")]
//        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{userId}")] HttpRequest req, string userId)
//        {
//            try
//            {             
//                var savedCourses = await _context.SavedCourses.Where(c => c.UserId == userId).ToListAsync();
//                return new OkObjectResult(savedCourses);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while fetching saved courses.");
//                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }
//}
