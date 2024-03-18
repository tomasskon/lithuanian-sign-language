using System.IO.Compression;
using System.Xml.Serialization;
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
    
    public async Task<IEnumerable<Guid>> GetUsersTrainingDataIdsAsync(Guid userId)
    {
        return await _trainingRepository.GetUsersTrainingDataIdsAsync(userId);
    }

    public async Task<byte[]> GetGroupedTrainingDataAsync()
    {
        var trainingData = await _trainingRepository.GetAll();
        var groupedBySignId = trainingData.GroupBy(x => x.SignId);
        
        var signNames = (await _signService.GetAllSignsAsync())
            .ToDictionary(k => k.Id, v => v.SignName);
        
        return CreateDatasetZipFile(signNames, groupedBySignId);
    }

    private static byte[] CreateDatasetZipFile(IReadOnlyDictionary<Guid, string> signNames, IEnumerable<IGrouping<Guid, TrainingData>> groupedData)
    {
        using var outputStream = new MemoryStream();
        using (var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
        {
            foreach (var group in groupedData)
            {
                var folderName = $"{signNames[group.Key]}";
                var fileCounter = 1;
                
                foreach (var trainingData in group)
                {
                    using var inputStream = new MemoryStream(trainingData.Data);
                    using var zipArchive = new ZipArchive(inputStream, ZipArchiveMode.Read);
                    
                    foreach (var entry in zipArchive.Entries)
                    {
                        var newEntry = archive.CreateEntry($"{folderName}/{fileCounter}.mp4");
                        
                        using (var entryStream = entry.Open())
                        using (var newEntryStream = newEntry.Open())
                        {
                            entryStream.CopyTo(newEntryStream);
                        }
                        
                        fileCounter++;
                    }
                }
            }
        }
        
        return outputStream.ToArray();
    }
}