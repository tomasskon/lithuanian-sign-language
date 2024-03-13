using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> UserLoginAsync(string emailAddress, string password);

        Task<string> UserRegisterAsync(User user, string password);
    }
}