using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();

    Task<User> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);

    Task<bool> ExistAsync(Guid id);
    
    Task<User> GetAsync(Guid id);
}