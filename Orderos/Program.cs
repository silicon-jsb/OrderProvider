using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orderos.Clients;
using Orderos.Data;
using Orderos.Repositories;

var host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Environment.CurrentDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();

		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		services.AddScoped<SavedCoursesRepository>();
		services.AddDbContext<DataContext>(options =>
		options.UseSqlServer(configuration.GetConnectionString("ORDERS_DATABASE")));




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
