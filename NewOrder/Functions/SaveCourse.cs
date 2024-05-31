using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewOrder.Data;
using NewOrder.Entities;

namespace NewOrder.Functions;

public class SaveOrder
{
    private readonly ILogger<SaveOrder> _logger;
    private readonly DataContext _context;

    public SaveOrder(ILogger<SaveOrder> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function("SaveCourse")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "courses")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var savedCourse = JsonConvert.DeserializeObject<SavedCoursesEntity>(requestBody);

        try
        {
            _context.SavedCourses.Add(savedCourse);
            await _context.SaveChangesAsync();

            return new OkObjectResult(savedCourse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving the course.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
