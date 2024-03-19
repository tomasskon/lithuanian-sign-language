using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface ITrainingService
{
    Task SubmitTrainingDataAsync(TrainingData trainingData);

    Task<IEnumerable<Guid>> GetUsersTrainingDataIdsAsync(Guid userId);

    Task<byte[]> GetGroupedTrainingDataAsync(Guid userId);
    
    Task DeleteUserTrainingDataAsync(Guid userId, Guid signId);
    
    Task<TrainingData> GetUserDataAsync(Guid userId, Guid signId);
}