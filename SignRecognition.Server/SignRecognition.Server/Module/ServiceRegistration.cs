﻿using SignRecognition.Domain.Interfaces;
using SignRecognition.Service.Services;

namespace SignRecognition.Server.Module;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}