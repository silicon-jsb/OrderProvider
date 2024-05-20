

namespace Orderos.Clients;

public class UserClient
{
	private readonly HttpClient _client;

	public UserClient(HttpClient client)
	{
		_client = client;
	}

	public async Task<UserEntity> GetUserById(string userId)
	{
		HttpResponseMessage response = await _client.GetAsync($"api/users/{userId}");
		return await response.Content.ReadAsAsync<UserEntity>();
	}
}
