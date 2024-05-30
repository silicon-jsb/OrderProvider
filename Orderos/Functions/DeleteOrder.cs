
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Orderos.Data;

//namespace Orderos.Functions;

//public class DeleteOrder
//{
//    private readonly ILogger<DeleteOrder> _logger;
//    private readonly DataContext _context;

//    public DeleteOrder(ILogger<DeleteOrder> logger, DataContext context)
//    {
//        _logger = logger;
//        _context = context;
//    }

//    [Function("DeleteOrder")]
//    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "orders/{id}")] HttpRequest req, string id)
//    {
//        var order = await _context.SavedCourses.FindAsync(id);
//        if (order == null)
//        {
//            return new NotFoundResult();
//        }

//        _context.SavedCourses.Remove(order);
//        await _context.SaveChangesAsync();

//        return new OkResult();
//    }
//}
