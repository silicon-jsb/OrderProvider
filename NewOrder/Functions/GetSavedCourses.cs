using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewOrder.Data;
using NewOrder.Data.GraphQL;
using NewOrder.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace NewOrder.Functions;
public class GetSavedCourses
{
    private readonly ILogger<GetSavedCourses> _logger;
    private readonly DataContext _context;
    private readonly HttpClient _httpClient;

    public GetSavedCourses(ILogger<GetSavedCourses> logger, DataContext context, HttpClient httpClient)
    {
        _logger = logger;
        _context = context;
        _httpClient = httpClient;
    }

    [Function("GetSavedCourses")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{userId}")] HttpRequest req, string userId)
    {
        try
        {
            var courses = await LoadCoursesForUser(userId);
            return new OkObjectResult(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching saved courses.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }


    public async Task<List<CourseDetailsModel>> LoadCoursesForUser(string userId)
    {
        var savedCourses = await _context.SavedCourses.Where(c => c.UserId == userId).ToListAsync();

        var courseDetailsTasks = savedCourses.Select(async savedCourse =>
        {
            var query = new GraphQLQuery
            {
                Query = @"
            query GetCourseById($id: String!) {
                getCourseById(id: $id) {
                    id
                    imageUri
                    imageHeaderUri
                    isBestseller
                    isDigital
                    categories
                    title
                    ingress
                    starRating
                    reviews
                    likes
                    likesInPercent
                    hours
                    authors {
                        name
                        authorImage
                    }
                    prices {
                        currency
                        price
                        discount
                    }
                    content {
                        description
                        includes
                        programDetails {
                            id
                            title
                            description
                        }
                    }
                }
            }",
                Variables = new { id = savedCourse.CourseId }
            };

            var response = await _httpClient.PostAsJsonAsync("https://courseprovider-silicon-jsb.azurewebsites.net/api/graphql?code=bgc2kUlGZk1Gvgj-j-Vsqb-KCjENeXD6H2b9-_4o5WkAAzFuW9ySgA%3D%3D", query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DynamicGraphQLResponse>();
                var courseDetails = JsonConvert.DeserializeObject<CourseDetailsModel>(result.Data.GetRawText());
                return courseDetails;
            }
            else
            {
                _logger.LogError($"Failed to load course details for course ID: {savedCourse.CourseId}");
                return null;
            }
        });

        var courseDetails = await Task.WhenAll(courseDetailsTasks);
        return courseDetails.Where(c => c != null).ToList();
    }

}
