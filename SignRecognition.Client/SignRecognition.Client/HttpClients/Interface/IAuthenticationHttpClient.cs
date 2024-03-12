using SignRecognition.Contract.Authentication;

namespace SignRecognition.Client.HttpClients.Interface;

public interface IAuthenticationHttpClient
{
    Task<string> RegisterUserAsync(UserRegisterContract userRegisterContract);

    Task<string> LoginUserAsync(UserLoginContract userLoginContract);
}