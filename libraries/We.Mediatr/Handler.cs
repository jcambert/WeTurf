using MediatR;
using We.Results;

namespace We.Mediatr;
public interface IQueryHandler<TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{

}
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : Response
{

}


public static class Handler
{
    public abstract class With<TQuery> : IQueryHandler<TQuery>
        where TQuery : IQuery
    {
        public abstract Task Handle(TQuery request, CancellationToken cancellationToken);
    }

    public abstract class With<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : Response
    {
        public abstract Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
        
    }
}