using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
}