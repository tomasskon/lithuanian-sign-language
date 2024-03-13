using SignRecognition.Domain.Models;

namespace SignRecognition.Repository.Repositories.Interface;

public interface ISignRepository
{
    Task UpsertListOfSignsAsync(IEnumerable<Sign> sings);
}