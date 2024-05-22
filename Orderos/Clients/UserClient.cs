

namespace Orderos.Clients;

public class UserClient
{
	private readonly HttpClient _client;

	public UserClient(HttpClient client)
	{
		_client = client;
	}

    //public async Task<UserDto> GetUserById(int userId)
    //{
    //    HttpResponseMessage response = await _client.GetAsync($"api/users/{userId}");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        return await response.Content.ReadAsAsync<UserDto>();
    //    }
    //    else
    //    {
    //        // Handle the error response
    //        // For example, throw an exception or return a default value
    //        return null;
    //    }
    //}
}
