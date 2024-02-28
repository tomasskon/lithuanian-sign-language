using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
}