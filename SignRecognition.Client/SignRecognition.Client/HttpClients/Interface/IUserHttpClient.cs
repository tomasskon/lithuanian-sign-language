namespace SignRecognition.Client.HttpClients.Interface;

public interface IUserHttpClient
{
    Task<bool> DeleteAsync();
}