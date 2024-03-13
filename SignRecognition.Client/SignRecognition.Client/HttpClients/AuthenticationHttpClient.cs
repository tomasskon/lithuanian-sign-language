using System.Net.Http.Json;
using Blazored.SessionStorage;
using SignRecognition.Client.HttpClients.Interface;
using SignRecognition.Contract.Authentication;

namespace SignRecognition.Client.HttpClients;

public class AuthenticationHttpClient : GenericHttpClient, IAuthenticationHttpClient
{
    public AuthenticationHttpClient(HttpClient httpClient, ISessionStorageService sessionStorageService) 
        : base(httpClient, sessionStorageService)
    {
    }

    public async Task<string> RegisterUserAsync(UserRegisterContract userRegisterContract)
    {
        var response = await HttpClient.PostAsJsonAsync("/Authentication/UserRegister", userRegisterContract);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }
    
    public async Task<string> LoginUserAsync(UserLoginContract userLoginContract)
    {
        var response = await HttpClient.PostAsJsonAsync("/Authentication/UserLogin", userLoginContract);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }
}