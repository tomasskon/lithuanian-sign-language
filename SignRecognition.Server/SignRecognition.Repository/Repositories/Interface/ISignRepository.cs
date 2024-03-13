using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface ISignRepository
{
    Task UpsertListAsync(IEnumerable<Sign> sings);

    Task<IEnumerable<Sign>> GetAllAsync();
}