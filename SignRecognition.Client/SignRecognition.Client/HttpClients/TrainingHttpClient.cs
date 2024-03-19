using System.IO.Compression;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using SignRecognition.Client.HttpClients.Interface;

namespace SignRecognition.Client.HttpClients;

public class TrainingHttpClient : GenericHttpClient, ITrainingHttpClient
{
    public TrainingHttpClient(HttpClient httpClient, ISessionStorageService sessionStorageService) 
        : base(httpClient, sessionStorageService)
    {
    }

    public async Task SubmitTrainingDataAsync(List<byte[]> data, Guid userId, Guid signId)
    {
        var httpClient = await GetAuthenticatedHttpClient();
        
        var request = new HttpRequestMessage(HttpMethod.Post, $"Training/SubmitTrainingData?userId={userId}&signId={signId}");
        var body = new ByteArrayContent(CreateZipArchive(data));
        body.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
        request.Content = body;

        await httpClient.SendAsync(request);
    }

    public async Task<IEnumerable<Guid>> GetUserSubmittedTrainingDataIdsAsync(Guid userId)
    {
        var httpClient = await GetAuthenticatedHttpClient();
        var response = await httpClient.GetAsync($"Training/GetUserSubmittedTrainingDataIds?userId={userId}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<IEnumerable<Guid>>();

        return null;
    }

    public async Task<bool> DeleteUserSignDataAsync(Guid signId)
    {
        var httpClient = await GetAuthenticatedHttpClient();
        var response = await httpClient.DeleteAsync($"Training/DeleteUserData?signId={signId}");

        return response.IsSuccessStatusCode;
    }

    public async Task<byte[]> GetUserData(Guid signId)
    {
        var httpClient = await GetAuthenticatedHttpClient();
        var response = await httpClient.GetAsync($"Training/GetUserDataFile?signId={signId}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();

        return null;
    }

    private static byte[] CreateZipArchive(IReadOnlyList<byte[]> byteArrays)
    {
        using var ms = new MemoryStream();
        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            for (var i = 0; i < byteArrays.Count; i++)
            {
                var entry = archive.CreateEntry($"{i}.webm");

                using var entryStream = entry.Open();
                entryStream.Write(byteArrays[i], 0, byteArrays[i].Length);
            }
        }

        return ms.ToArray();
    }
}