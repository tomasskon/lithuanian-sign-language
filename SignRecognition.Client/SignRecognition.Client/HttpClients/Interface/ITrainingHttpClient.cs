namespace SignRecognition.Client.HttpClients.Interface;

public interface ITrainingHttpClient
{
    Task SubmitTrainingDataAsync(List<byte[]> data, Guid userId, Guid signId);

    Task<IEnumerable<Guid>> GetUserSubmittedTrainingDataIdsAsync(Guid userId);
}