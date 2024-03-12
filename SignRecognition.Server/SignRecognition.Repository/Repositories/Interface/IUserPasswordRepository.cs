using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface
{
    public interface IUserPasswordRepository
    {
        Task<Guid> Create(string passwordHash, byte[] passwordSalt, Guid userId);

        Task<UserPassword> GetByUserId(Guid userId);
    }
}