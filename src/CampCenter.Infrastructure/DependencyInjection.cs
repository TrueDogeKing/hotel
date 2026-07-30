using CampCenter.Application.Interfaces;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Auth;
using CampCenter.Infrastructure.Email;
using CampCenter.Infrastructure.Payments;
using CampCenter.Infrastructure.Persistence;
using CampCenter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampCenter.Infrastructure;

/// Registration of infrastructure services in the DI container.
public static class DependencyInjection
{
    /// Registers <see cref="Persistence.AppDbContext"/> with a PostgreSQL (Npgsql) provider
    /// and other infrastructure services.
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<ITokenService, JwtTokenService>();

        services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddSingleton<IEmailSender, SmtpEmailSender>();
        services.AddHttpClient<IPaymentGateway, Przelewy24Client>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IRoomTaskRepository, RoomTaskRepository>();
        services.AddScoped<IRoomCleaningRepository, RoomCleaningRepository>();
        services.AddScoped<IClosureRepository, ClosureRepository>();
        services.AddScoped<IMealTimeDefaultRepository, MealTimeDefaultRepository>();
        services.AddScoped<IScheduleEntryRepository, ScheduleEntryRepository>();
        services.AddScoped<IBookingMealTimeRepository, BookingMealTimeRepository>();

        return services;
    }
}
