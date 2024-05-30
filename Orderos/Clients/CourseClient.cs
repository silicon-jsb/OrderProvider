using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using NewOrder.Models;
using System.Threading.Tasks;

namespace NewOrder.Clients
{
    public class CourseClient
    {
        private readonly HttpClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly Dictionary<int, Course> _courses = new Dictionary<int, Course>();


        public CourseClient(HttpClient client, ServiceBusClient serviceBusClient, string OrderQ)
        {
            _client = client;
            _processor = serviceBusClient.CreateProcessor(OrderQ);
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
            Course? course = JsonConvert.DeserializeObject<Course>(courseData);

            _courses[course.Id] = course;

            Console.WriteLine($"Received course with ID {course.Id} and title {course.Title}");

            return args.CompleteMessageAsync(args.Message);
        }


        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public Course GetCourseById(int courseId)
        {
            _courses.TryGetValue(courseId, out var course);
            return course!;
        }

    }
}

