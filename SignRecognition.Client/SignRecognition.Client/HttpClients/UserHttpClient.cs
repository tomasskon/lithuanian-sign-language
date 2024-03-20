using Blazored.SessionStorage;
using SignRecognition.Client.HttpClients.Interface;

namespace SignRecognition.Client.HttpClients;

public class UserHttpClient : GenericHttpClient, IUserHttpClient
{
    public UserHttpClient(HttpClient httpClient, ISessionStorageService sessionStorageService) 
        : base(httpClient, sessionStorageService)
    {
    }
    
    public async Task<bool> DeleteAsync()
    {
        var httpClient = await GetAuthenticatedHttpClient();
        var response = await httpClient.DeleteAsync("/User/Delete");

        return response.IsSuccessStatusCode;
    }
}