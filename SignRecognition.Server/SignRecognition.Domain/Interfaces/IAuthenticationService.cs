using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> UserLogin(string emailAddress, string password);

        Task<string> UserRegister(User user, string password);
    }
}