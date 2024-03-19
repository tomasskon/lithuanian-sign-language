namespace SignRecognition.Client.HttpClients.Interface;

public interface ITrainingHttpClient
{
    Task SubmitTrainingDataAsync(List<byte[]> data, Guid userId, Guid signId);

    Task<IEnumerable<Guid>> GetUserSubmittedTrainingDataIdsAsync(Guid userId);

    Task<bool> DeleteUserSignDataAsync(Guid signId);
    
    Task<byte[]> GetUserData(Guid signId);
}