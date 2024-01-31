using Application.Abstractions.Login;
using Application.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Data.RepositoriesImpl;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IInitialSessionRepository, InitialInitialSessionRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}