using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SignRecognition.Client.Models;
using SignRecognition.Client.Services.Interfaces;

namespace SignRecognition.Client.Services;

public class AuthStateProviderService : AuthenticationStateProvider, IDisposable
{
    private readonly IAuthenticationService _authenticationService;
    private AuthenticatedUser _currentUser;
    
    public AuthStateProviderService(IAuthenticationService authenticationService)
    {
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        _authenticationService = authenticationService;
    }

    [JSInvokable]
    public async Task GoogleLogin(GoogleAuthResponse googleAuthResponse)
    {
        var user = await _authenticationService.AuthenticateUser(googleAuthResponse.Credential);
        _currentUser = user;

        if (user is not null)
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(ToClaimsPrincipal())));
    }
    
    public async Task Logout()
    {
        await _authenticationService.LogoutUser();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = await _authenticationService.GetUserDataFromStoredToken();

        if (user is null) 
            return new AuthenticationState(principal);
        
        _currentUser = user;
        principal = ToClaimsPrincipal();

        return new AuthenticationState(principal);
    }
    
    public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    
    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;
        _currentUser = _authenticationService.AuthenticatedUserFromClaimsPrincipal(authenticationState.User);
    }

    private ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, _currentUser.Email),
        }, 
        "SignRecognition"));
}
