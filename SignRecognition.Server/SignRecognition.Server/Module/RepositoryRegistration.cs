using SignRecognition.Repository.Repositories;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Server.Module;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserPasswordRepository, UserPasswordRepository>();
        services.AddTransient<ISignRepository, SignRepository>();
        services.AddTransient<ITrainingRepository, TrainingRepository>();
        
        return services;
    }
}