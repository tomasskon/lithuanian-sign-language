using SignRecognition.Domain.Interfaces;
using SignRecognition.Service.Services;

namespace SignRecognition.Server.Module;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ISignService, SignService>();
        services.AddScoped<ITrainingService, TrainingService>();
        
        return services;
    }
}