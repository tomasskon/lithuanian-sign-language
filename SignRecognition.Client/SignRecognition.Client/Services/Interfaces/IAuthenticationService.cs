using System.Security.Claims;
using SignRecognition.Client.Models;

namespace SignRecognition.Client.Services.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticatedUser> AuthenticateUser(string token);
    Task LogoutUser();
    Task<AuthenticatedUser> GetUserDataFromStoredToken();
    AuthenticatedUser AuthenticatedUserFromClaimsPrincipal(ClaimsPrincipal principal);
}