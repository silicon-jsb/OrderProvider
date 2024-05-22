using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace Orderos.Clients
{
    public class CourseClient
    {
        private readonly HttpClient _client;
        private readonly ServiceBusProcessor _processor;

        public CourseClient(HttpClient client, ServiceBusClient serviceBusClient)
        {
            _client = client;
            _processor = serviceBusClient.CreateProcessor("CourseProvider");
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
        }

        private Task MessageHandler(ProcessMessageEventArgs args)
        {
            
            string courseData = args.Message.Body.ToString();

            // TODO: Update your application state or display the updated course information to the user
      
            return args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // TODO: Handle the error
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}

