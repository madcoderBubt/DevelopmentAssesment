using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagement.CQRS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCQRSFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        // Register Dispatcher
        services.AddScoped<Dispatcher>();

        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract);

        foreach (var type in types)
        {
            // Register Query Handlers: IQueryHandler<TQuery, TResult>
            var queryInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                       i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

            foreach (var iface in queryInterfaces)
            {
                services.AddScoped(iface, type);
            }

            // Register Command Handlers: ICommandHandler<TCommand>
            var commandInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                       i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

            foreach (var iface in commandInterfaces)
            {
                services.AddScoped(iface, type);
            }

            // Register Request Handlers: IRequestHandler<TRequest, TResult>
            var requestInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                       i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            foreach (var iface in requestInterfaces)
            {
                services.AddScoped(iface, type);
            }
        }

        return services;
    }
}