using System.Net.Http.Json;
using SignRecognition.Client.HttpClients.Interface;
using SignRecognition.Contract.Authentication;

namespace SignRecognition.Client.HttpClients;

public class AuthenticationHttpClient : IAuthenticationHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthenticationHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterUserAsync(UserRegisterContract userRegisterContract)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44328/Authentication/UserRegister", userRegisterContract);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }
    
    public async Task<string> LoginUserAsync(UserLoginContract userLoginContract)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44328/Authentication/UserLogin", userLoginContract);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }
}