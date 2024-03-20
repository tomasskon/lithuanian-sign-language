using System.Dynamic;
using MailSystem.Exception;
using SignRecognition.Domain.Exceptions;
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

    public async Task<User> CreateAsync(User user)
    {
        if (await _userRepository.CheckIfEmailIsUsedAsync(user.Email))
            throw new UserEmailAlreadyUsedException($"User email already used: {user.Email}");

        return await _userRepository.CreateAsync(user);
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
            throw new UserNotFoundException();

        return user;
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _userRepository.ExistAsync(id);
    }

    public async Task<User> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);

        if (user == null)
            throw new UserNotFoundException($"User not found: {id}");

        return user;
    }

    public async Task DeleteAllDataAsync(Guid userId)
    {
        await _userRepository.DeleteAsync(userId);
    }
}