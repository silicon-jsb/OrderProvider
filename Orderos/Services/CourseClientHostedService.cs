using Microsoft.Extensions.Hosting;
using Orderos.Clients;

namespace Orderos.Services;

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


