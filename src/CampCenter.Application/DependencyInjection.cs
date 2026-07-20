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
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ICampSessionService, CampSessionService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAdminBookingService, AdminBookingService>();
        services.AddScoped<IRoomTaskService, RoomTaskService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
