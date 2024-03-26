using System.Net.Http.Json;
using Blazored.SessionStorage;
using SignRecognition.Client.HttpClients.Interface;
using SignRecognition.Contract;
using SignRecognition.Contract.Authentication;

namespace SignRecognition.Client.HttpClients;

public class AuthenticationHttpClient : GenericHttpClient, IAuthenticationHttpClient
{
    public AuthenticationHttpClient(HttpClient httpClient, ISessionStorageService sessionStorageService) 
        : base(httpClient, sessionStorageService)
    {
    }

    public async Task<Tuple<string, ErrorResponseContract>> RegisterUserAsync(UserRegisterContract userRegisterContract)
    {
        var response = await HttpClient.PostAsJsonAsync("/Authentication/UserRegister", userRegisterContract);

        if (response.IsSuccessStatusCode)
            return new (await response.Content.ReadAsStringAsync(), null);

        return new (null, await response.Content.ReadFromJsonAsync<ErrorResponseContract>());
    }
    
    public async Task<Tuple<string, ErrorResponseContract>> LoginUserAsync(UserLoginContract userLoginContract)
    {
        var response = await HttpClient.PostAsJsonAsync("/Authentication/UserLogin", userLoginContract);

        if (response.IsSuccessStatusCode)
            return new (await response.Content.ReadAsStringAsync(), null);

        return new (null, await response.Content.ReadFromJsonAsync<ErrorResponseContract>());
    }
}