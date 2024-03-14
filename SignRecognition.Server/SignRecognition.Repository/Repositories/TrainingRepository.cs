using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories;

public class TrainingRepository : GenericRepository<TrainingDataEntity>, ITrainingRepository
{
    public TrainingRepository(DatabaseContext context, IMapper repoMapper) 
        : base(context, repoMapper)
    {
    }

    public async Task RenewTrainingDataAsync(TrainingData trainingData)
    {
        var dataToRemove = await Set.SingleOrDefaultAsync(x => x.UserId == trainingData.UserId && x.SignId == trainingData.SignId);
        
        if(dataToRemove is not null)
            Set.Remove(dataToRemove);

        var dataToAdd = RepoMapper.Map<TrainingDataEntity>(trainingData);
        await Set.AddAsync(dataToAdd);

        await Context.SaveChangesAsync();
    }
}