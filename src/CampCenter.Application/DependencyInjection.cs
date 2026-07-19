using CampCenter.Application.Interfaces;
using CampCenter.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CampCenter.Application;

public static class DependencyInjection
{
    /// Register services and validators application layer.
    /// <param name="services">Service collection.</param>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
