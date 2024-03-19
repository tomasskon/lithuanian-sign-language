using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    
    Task<User> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);

    Task<bool> CheckIfEmailIsUsedAsync(string email);

    Task<bool> ExistAsync(Guid id);
    
    Task<User> GetAsync(Guid id);
}