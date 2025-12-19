using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastracture.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectManagement.CQRS;

public class Dispatcher
{
    private readonly IServiceProvider _provider;

    public Dispatcher(IServiceProvider provider) => _provider = provider;

    public async Task Handle<TCommand>(TCommand command)
    {
        await ValidateAsync(command);

        var handler = _provider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command);
    }

    public async Task<TResult> Handle<TQuery, TResult>(TQuery query)
    {
        await ValidateAsync(query);

        var handler = _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query);
    }

    public async Task<TResult> HandleRequest<TQuery, TResult>(TQuery query)
    {
        await ValidateAsync(query);

        var handler = _provider.GetRequiredService<IRequestHandler<TQuery, TResult>>();
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {typeof(TQuery).Name}");

        return await handler.Handle(query);
    }

    public async Task<TResult> HandleRequest<TResult>(object query)
    {
        await ValidateAsync(query);

        var queryType = query.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(queryType, typeof(TResult));

        var handler = _provider.GetRequiredService(handlerType) as dynamic;
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {queryType.Name}");

        // Use reflection to call Handle method
        var handleMethod = handlerType.GetMethod("Handle");
        return await (dynamic)handleMethod.Invoke(handler, new object[] { query, CancellationToken.None });
    }

    /// <summary>
    /// Validates non-generic object (for HandleRequest with object parameter)
    /// </summary>
    private async Task ValidateAsync(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var objectType = obj.GetType();
        var validatorType = typeof(IValidator<>).MakeGenericType(objectType);
        var validator = _provider.GetService(validatorType) as IValidator;

        if (validator == null)
            return;

        var validationContext = new ValidationContext<object>(obj);
        var validationResult = await validator.ValidateAsync(validationContext);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors);
        }
    }

    /// <summary>
    /// Validates the request/command/query using FluentValidation if a validator is registered.
    /// Throws CqrsValidationException if validation fails.
    /// </summary>
    private async Task ValidateAsync<T>(T obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var validator = _provider.GetService<IValidator<T>>();

        if (validator == null)
            return;

        var validationResult = await validator.ValidateAsync(obj);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors);
        }
    }
}
