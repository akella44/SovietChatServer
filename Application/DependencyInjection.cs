using Application.Abstractions.Cryptography;
using Application.Cryptography.Providers;
using Domain.Cryptography;
using MediatR;

namespace Application;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            cf => cf.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));

        services.AddTransient<RsaProvider>();
        services.AddTransient<AesProvider>();
        
        services.AddTransient<IRequestHandler<EncryptCommand<RsaProvider>, string>, EncryptCommandHandler<RsaProvider>>();
        services.AddTransient<IRequestHandler<EncryptCommand<AesProvider>, string>, EncryptCommandHandler<AesProvider>>();
        
        services.AddTransient<IRequestHandler<DecryptCommand<AesProvider>, string>, DecryptCommandHandler<AesProvider>>();
        return services;
    }
}