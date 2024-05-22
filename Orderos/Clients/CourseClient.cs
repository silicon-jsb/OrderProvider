
namespace Orderos.Clients;

public class CourseClient
{
	private readonly HttpClient _client;

	public CourseClient(HttpClient client)
	{
		_client = client;
	}

	//public async Task<CourseEntity> GetCourseById(int courseId)
	//{
	//	HttpResponseMessage response = await _client.GetAsync($"api/courses/{courseId}");

	//	if (response.IsSuccessStatusCode)
	//	{
	//		return await response.Content.ReadAsAsync<CourseEntity>();
	//	}
	//	else
	//	{
	//		// Handle the error response
	//		// For example, throw an exception or return a default value
	//		return null;
	//	}
	//}

	
}
