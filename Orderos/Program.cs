using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewOrder.Clients;
using NewOrder.Data;
using NewOrder.Repositories;
using NewOrder.Services;

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
		services.AddScoped<UserService>();
        services.AddHostedService<CourseClientHostedService>();
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ORDERS_DATABASE")));


        services.AddHttpClient<UserClient>(client =>
        {
            client.BaseAddress = new Uri("https://orderprovider-newsilicon-jsb.azurewebsites.net/api/users?code=vemQliZViS9smoTmYziSkRYrHLFxm5Q1RMKMSISjO5CUAzFu0W6oGA==");
        });
        services.AddHttpClient<UserClient>(client =>
		{
			client.BaseAddress = new Uri("https://userprovider-newsilicon-jsb.azurewebsites.net/api/users?code=vemQliZViS9smoTmYziSkRYrHLFxm5Q1RMKMSISjO5CUAzFu0W6oGA==");
		});
			
		
       

    })
	.Build();

host.Run();
