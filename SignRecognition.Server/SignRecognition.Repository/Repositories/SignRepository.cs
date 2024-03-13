using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories;

public class SignRepository : GenericRepository<SignEntity>, ISignRepository
{
    public SignRepository(DatabaseContext context, IMapper repoMapper) 
        : base(context, repoMapper)
    {
    }

    public async Task UpsertListAsync(IEnumerable<Sign> sings)
    {
        var signsToUpsert = sings.ToDictionary(x => x.SignName, v => v);

        var signsToUpdate = 
            await Set.Where(x => signsToUpsert.Keys.Contains(x.SignName))
                .ToListAsync();

        foreach (var entity in signsToUpdate)
        {
            entity.SignVideoUrl = signsToUpsert[entity.SignName].SignVideoUrl;
            signsToUpsert.Remove(entity.SignName);
        }

        var entitiesToAdd = signsToUpsert.Values.Select(x => RepoMapper.Map<SignEntity>(x));
        await Set.AddRangeAsync(entitiesToAdd);

        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Sign>> GetAllAsync()
    {
        var signEntities = await Set.ToListAsync();

        return RepoMapper.Map<IEnumerable<Sign>>(signEntities);
    }
}   