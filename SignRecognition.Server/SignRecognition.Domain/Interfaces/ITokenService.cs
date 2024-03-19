using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetJwtToken(User user);

        Guid GetUserIdFromToken(string jwtToken);
    }
}