using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}