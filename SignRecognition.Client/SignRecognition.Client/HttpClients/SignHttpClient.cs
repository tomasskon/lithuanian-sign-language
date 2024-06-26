﻿using System.Net.Http.Json;
using Blazored.SessionStorage;
using SignRecognition.Client.HttpClients.Interface;
using SignRecognition.Contract.Signs;

namespace SignRecognition.Client.HttpClients;

public class SignHttpClient : GenericHttpClient, ISignHttpClient
{
    public SignHttpClient(HttpClient httpClients, ISessionStorageService sessionStorageService) 
        : base(httpClients, sessionStorageService)
    {
    }
    
    public async Task<IEnumerable<SignContract>> GetAllSignsAsync()
    {
        var httpClient = await GetAuthenticatedHttpClient();
        
        var response = await httpClient.GetAsync("Sign/GetAll");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<IEnumerable<SignContract>>();

        return null;
    }

    public async Task<SignContract> GetSignAsync(Guid signId)
    {
        var httpClient = await GetAuthenticatedHttpClient();

        var response = await httpClient.GetAsync($"Sign/Get?signId={signId}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<SignContract>();

        return null;
    }
}