using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface ISignService
{
    Task AddSignsAsync(IEnumerable<Sign> signs);

    Task<IEnumerable<Sign>> GetAllSignsAsync();

    Task<bool> ExistsAsync(Guid id);
}