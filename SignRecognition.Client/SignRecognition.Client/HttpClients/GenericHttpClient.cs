using System.Net.Http.Headers;
using Blazored.SessionStorage;
using Microsoft.IdentityModel.Tokens;

namespace SignRecognition.Client.HttpClients;

public class GenericHttpClient
{
    protected readonly HttpClient HttpClient;
    private readonly ISessionStorageService _sessionStorageService;

    protected GenericHttpClient(HttpClient httpClient, ISessionStorageService sessionStorageService)
    {
        HttpClient = httpClient;
        _sessionStorageService = sessionStorageService;
    }

    protected async Task<HttpClient> GetAuthenticatedHttpClient()
    {
        var authToken = await _sessionStorageService.GetItemAsStringAsync("token");
        
        if(!authToken.IsNullOrEmpty())
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        return HttpClient;
    }
}