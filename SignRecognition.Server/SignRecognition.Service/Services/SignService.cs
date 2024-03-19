using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Service.Services;

public class SignService : ISignService
{
    private readonly ISignRepository _signRepository;

    public SignService(ISignRepository signRepository)
    {
        _signRepository = signRepository;
    }

    public async Task AddSignsAsync(IEnumerable<Sign> signs)
    {
        await _signRepository.UpsertListAsync(signs);
    }

    public async Task<IEnumerable<Sign>> GetAllSignsAsync()
    {
        return await _signRepository.GetAllAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _signRepository.ExistsAsync(id);
    }

    public async Task<Sign> GetSignAsync(Guid signId)
    {
        var sign = await _signRepository.GetAsync(signId);

        if (sign is null)
            throw new SignNotFoundException($"Sign id: {signId}");

        return sign;
    }
}