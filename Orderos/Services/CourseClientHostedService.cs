using Microsoft.Extensions.Hosting;
using NewOrder.Clients;

namespace NewOrder.Services;

public class CourseClientHostedService : IHostedService
{
    private readonly CourseClient _courseClient;

    public CourseClientHostedService(CourseClient courseClient)
    {
        _courseClient = courseClient;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _courseClient.StartProcessingAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _courseClient.StopProcessingAsync();
    }
}


