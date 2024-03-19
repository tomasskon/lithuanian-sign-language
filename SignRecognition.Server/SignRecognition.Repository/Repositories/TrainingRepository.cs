using System.Security;
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

    public async Task<IEnumerable<Guid>> GetUsersTrainingDataIdsAsync(Guid userId)
    {
        return await Set
            .Where(x => x.UserId == userId)
            .Select(x => x.SignId)
            .ToListAsync(); 
    }

    public async Task<IEnumerable<TrainingData>> GetAll()
    {
        var entities = await Set.ToListAsync();

        return RepoMapper.Map<IEnumerable<TrainingData>>(entities);
    }

    public async Task DeleteAsync(Guid userId, Guid signId)
    {
        var entity = await Set.SingleOrDefaultAsync(x => x.UserId == userId && x.SignId == signId);
        
        Set.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<TrainingData> GetAsync(Guid userId, Guid signId)
    {
        var entity = await Set.SingleOrDefaultAsync(x => x.UserId == userId && x.SignId == signId);

        return RepoMapper.Map<TrainingData>(entity);
    }
}