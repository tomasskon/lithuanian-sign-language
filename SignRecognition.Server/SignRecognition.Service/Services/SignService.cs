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
        await _signRepository.UpsertListOfSignsAsync(signs);
    }
}