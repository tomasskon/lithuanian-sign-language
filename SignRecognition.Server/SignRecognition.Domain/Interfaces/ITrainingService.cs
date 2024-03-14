using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface ITrainingService
{
    Task SubmitTrainingDataAsync(TrainingData trainingData);
}