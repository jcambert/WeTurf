using MediatR;
using System.Threading;
using System.Threading.Tasks;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface ICommand<T>:IRequest<Result<T>>
    where T : notnull
{
}

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : IRequest<Result<TResult>>
    where TResult : notnull
{

}

public abstract class BaseHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : IRequest<Result<TResult>>
    where TResult : notnull
{
    public abstract Task<Result<TResult>> Handle(TCommand request, CancellationToken cancellationToken);
    
}