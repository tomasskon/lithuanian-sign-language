using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(DatabaseContext context, IMapper repoMapper) 
        : base(context, repoMapper)
    {
    }

    public async Task<User> CreateAsync(User user)
    {
        var userEntity = RepoMapper.Map<UserEntity>(user);

        await Set.AddAsync(userEntity);
        await Context.SaveChangesAsync();

        return RepoMapper.Map<User>(userEntity);
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        var userEntities = await Set.ToListAsync();

        return RepoMapper.Map<List<User>>(userEntities);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var userEntity = await Set.SingleOrDefaultAsync(x => x.Email == email);

        return RepoMapper.Map<User>(userEntity);
    }

    public async Task<bool> CheckIfEmailIsUsedAsync(string email)
    {
        return await Set.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await Set.AnyAsync(x => x.Id == id);
    }

    public async Task<User> GetAsync(Guid id)
    {
        var userEntity = await Set.SingleOrDefaultAsync(x => x.Id == id);

        return RepoMapper.Map<User>(userEntity);
    }
}