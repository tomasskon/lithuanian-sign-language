﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.SessionStorage;
using SignRecognition.Client.Models;
using SignRecognition.Client.Services.Interfaces;

namespace SignRecognition.Client.Services;

public class AuthenticationService : Interfaces.IAuthenticationService
{
    private readonly ISessionStorageService _sessionStorageService;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthenticationService(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
        _tokenHandler = new JwtSecurityTokenHandler();
    }
    
    public async Task<AuthenticatedUser> AuthenticateUser(string token)
    {
        if (!_tokenHandler.CanReadToken(token)) 
            return null;
        
        var jwtSecurityToken = _tokenHandler.ReadJwtToken(token);
        await _sessionStorageService.SetItemAsync("token", token);

        return new AuthenticatedUser
        {
            Email = jwtSecurityToken.Claims.First(c => c.Type == "email").Value,
        };
    }
    
    public async Task LogoutUser() => await _sessionStorageService.RemoveItemAsync("token");

    public async Task<AuthenticatedUser> GetUserDataFromStoredToken()
    {
        var token = await _sessionStorageService.GetItemAsStringAsync("token");

        if (token is "" or null)
            return null;
        
        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);
        var user = AuthenticatedUserFromClaimsPrincipal(claimsPrincipal);

        return user;
    }
    
    public AuthenticatedUser AuthenticatedUserFromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        Email = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
    };

    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        var identity = new ClaimsIdentity();

        if (!_tokenHandler.CanReadToken(token)) 
            return new ClaimsPrincipal(identity);
        
        var jwtSecurityToken = _tokenHandler.ReadJwtToken(token);
        identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Sign Recognition");

        return new ClaimsPrincipal(identity);
    }
}