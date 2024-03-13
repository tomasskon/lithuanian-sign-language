using SignRecognition.Contract.Signs;

namespace SignRecognition.Client.HttpClients.Interface;

public interface ISignHttpClient
{
    Task<IEnumerable<SignContract>> GetAllSignsAsync();
}