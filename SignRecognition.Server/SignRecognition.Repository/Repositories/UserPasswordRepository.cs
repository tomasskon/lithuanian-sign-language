using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories
{
    public class UserPasswordRepository : GenericRepository<UserPasswordEntity>, IUserPasswordRepository
    {
        
        public UserPasswordRepository(DatabaseContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> Create(string passwordHash, byte[] passwordSalt, Guid userId)
        {
            var userPasswordEntity = new UserPasswordEntity
            {
                UserId = userId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await Set.AddAsync(userPasswordEntity);
            await Context.SaveChangesAsync();

            return userPasswordEntity.Id;
        }

        public async Task<UserPassword> GetByUserId(Guid userId)
        {
            var passwordEntity = await Set.SingleOrDefaultAsync(x => x.User.Id == userId);

            return RepoMapper.Map<UserPassword>(passwordEntity);
        }
    }
}