using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewOrder.Data;


namespace NewOrder.Functions;

public class GetOrders
{
    private readonly ILogger<GetOrders> _logger;
    private readonly DataContext _context;

    public GetOrders(ILogger<GetOrders> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function("GetOrders")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders")] HttpRequest req)
    {
        try
        {
            var savedCourses = await _context.SavedCourses.ToListAsync();
            return new OkObjectResult(savedCourses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching saved courses.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
