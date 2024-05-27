using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orderos.Clients;
using Orderos.Data;
using Orderos.Repositories;
using Orderos.Services;

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
		services.AddScoped<CourseService>();
		services.AddScoped<UserService>();
        services.AddHostedService<CourseClientHostedService>();
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ORDERS_DATABASE")));


        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var serviceBusConnectionString = configuration.GetConnectionString("ServiceBusConnectionString");
            return new ServiceBusClient(serviceBusConnectionString);
        });
       
        services.AddSingleton<CourseClient>(sp => new CourseClient(new HttpClient(), 
			new ServiceBusClient("Endpoint=sb://courseprovider.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Q/rrVPV452ftyNuC9dEJDV84IoWUbvz8l+ASbDvjztg="), "OrderQ"));



        services.AddHttpClient<UserClient>(client =>
		{
			client.BaseAddress = new Uri("https://userprovider-newsilicon-jsb.azurewebsites.net/api/users?code=vemQliZViS9smoTmYziSkRYrHLFxm5Q1RMKMSISjO5CUAzFu0W6oGA==");
		});
			
		
       

    })
	.Build();

host.Run();
