using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		//services.AddDbContext<DataContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("ORDERS_DATABASE")));
	})
	.Build();

host.Run();
