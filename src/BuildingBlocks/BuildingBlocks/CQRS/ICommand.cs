using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Empty command, does not return anything
/// </summary>
public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
