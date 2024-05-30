using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewOrder.Data;

namespace NewOrder.Functions;

public class FetchOneOrderId
{
    private readonly ILogger<FetchOneOrderId> _logger;
    private readonly DataContext _context;

    public FetchOneOrderId(ILogger<FetchOneOrderId> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function("FetchOneOrderId")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{id}")] HttpRequest req, int id)
    {
        var order = await _context.SavedCourses.FindAsync(id);
        if (order == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(order);
    }
}
