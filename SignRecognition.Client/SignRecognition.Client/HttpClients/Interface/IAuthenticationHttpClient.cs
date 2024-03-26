using SignRecognition.Contract;
using SignRecognition.Contract.Authentication;

namespace SignRecognition.Client.HttpClients.Interface;

public interface IAuthenticationHttpClient
{
    Task<Tuple<string, ErrorResponseContract>> RegisterUserAsync(UserRegisterContract userRegisterContract);

    Task<Tuple<string, ErrorResponseContract>> LoginUserAsync(UserLoginContract userLoginContract);
}