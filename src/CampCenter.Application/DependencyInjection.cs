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
        services.AddScoped<IClosureService, ClosureService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAdminBookingService, AdminBookingService>();
        services.AddScoped<IRoomTaskService, RoomTaskService>();
        services.AddScoped<IHousekeepingService, HousekeepingService>();
        // Przelewy24 is switched off — the owner records payments by hand. Left
        // registered nowhere rather than deleted, so turning it back on is a
        // matter of uncommenting this line and its infrastructure counterpart.
        // services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPricingService, PricingService>();
        services.AddScoped<IMealTimeService, MealTimeService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
