using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface
{
    public interface IUserPasswordRepository
    {
        Task<Guid> CreateAsync(string passwordHash, byte[] passwordSalt, Guid userId);

        Task<UserPassword> GetByUserIdAsync(Guid userId);
    }
}