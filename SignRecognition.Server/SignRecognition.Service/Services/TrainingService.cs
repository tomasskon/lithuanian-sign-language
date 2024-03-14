using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Service.Services;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IUserService _userService;
    private readonly ISignService _signService;

    public TrainingService(ITrainingRepository trainingRepository, IUserService userService, ISignService signService)
    {
        _trainingRepository = trainingRepository;
        _userService = userService;
        _signService = signService;
    }

    public async Task SubmitTrainingDataAsync(TrainingData trainingData)
    {
        if (!await _userService.ExistAsync(trainingData.UserId))
            throw new UserNotFoundException($"User not found: {trainingData.UserId}");

        if (!await _signService.ExistsAsync(trainingData.SignId))
            throw new SignNotFoundException($"Sign not found: {trainingData.SignId}");

        await _trainingRepository.RenewTrainingDataAsync(trainingData);
    }
}