//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Logging;
//using Orderos.Services;

//namespace Orderos.Functions;

//public class UserFetcher : FunctionBase
//{
//    private readonly ILogger<UserFetcher> _logger;
//    private readonly CourseService _courseService;
//    private readonly UserService _userService;

//    public UserFetcher(ILogger<UserFetcher> logger, CourseService courseService, UserService userService)
//    {
//        _logger = logger;
//        _courseService = courseService;
//        _userService = userService;
//    }

//    [Function("UserFetcher")]
//    public async Task<IActionResult> Run(
//        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
//    {
//        var users = await _userService.GetUsers();

        
//        foreach (var user in users)
//        {
//            user.SavedCourses = await _courseService.GetAllSavedCourses(user.Id);
//        }

//        _logger.LogInformation("C# HTTP trigger function processed a request.");
//        return new OkObjectResult(users);
//    }
//}
