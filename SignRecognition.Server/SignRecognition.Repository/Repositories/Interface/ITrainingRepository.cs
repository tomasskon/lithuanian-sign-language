using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface ITrainingRepository
{
    Task RenewTrainingDataAsync(TrainingData trainingData);

    Task<IEnumerable<Guid>> GetUsersTrainingDataIdsAsync(Guid userId);

    Task<IEnumerable<TrainingData>> GetAll();
    
    Task DeleteAsync(Guid userId, Guid signId);
    
    Task<TrainingData> GetAsync(Guid userId, Guid signId);
}