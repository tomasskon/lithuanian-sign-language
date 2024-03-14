using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface ITrainingRepository
{
    Task RenewTrainingDataAsync(TrainingData trainingData);
}