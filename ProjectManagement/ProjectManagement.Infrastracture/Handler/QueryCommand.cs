namespace ProjectManagement.CQRS;

// Interfaces
public interface ICommandHandler<TCommand>
{
    Task Handle(TCommand command);
}

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> Handle(TQuery query);
}

public interface IRequestHandler<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}