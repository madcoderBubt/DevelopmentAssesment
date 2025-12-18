using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectManagement.CQRS;

public class Dispatcher
{
    private readonly IServiceProvider _provider;

    public Dispatcher(IServiceProvider provider) => _provider = provider;

    public async Task Handle<TCommand>(TCommand command)
    {
        var handler = _provider.GetService<ICommandHandler<TCommand>>();
        await handler.Handle(command);
    }

    public async Task<TResult> Handle<TQuery, TResult>(TQuery query)
    {
        var handler = _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query);
    }

    public async Task<TResult> HandleRequest<TQuery, TResult>(TQuery query)
    {
        var handler = _provider.GetService<IRequestHandler<TQuery, TResult>>();
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {typeof(TQuery).Name}");

        return await handler.Handle(query);
    }

    public async Task<TResult> HandleRequest<TResult>(object query)
    {
        var queryType = query.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(queryType, typeof(TResult));

        var handler = _provider.GetService(handlerType) as dynamic;
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {queryType.Name}");

        // Use reflection to call Handle method
        var handleMethod = handlerType.GetMethod("Handle");
        return await (dynamic)handleMethod.Invoke(handler, new object[] { query, CancellationToken.None });
    }
}
