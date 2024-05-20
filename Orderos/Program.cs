using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orderos.Clients;

var host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();

		services.AddHttpClient<UserClient>(client =>
		{
			client.BaseAddress = new Uri("https://your-user-service-url");
		});
			
		services.AddHttpClient<CourseClient>(client =>
		{
			client.BaseAddress = new Uri("https://your-course-service-url");
		});
	})
	.Build();

host.Run();
