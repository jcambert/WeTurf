namespace We.Mediatr;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery> where TQuery : IQuery { }

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : Response
{ }

public static class Handler
{
    public abstract class With<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
#if MEDIATR
        public abstract Task Handle(TQuery request, CancellationToken cancellationToken);
#endif
#if MEDIATOR
        public abstract ValueTask<Unit> Handle(TQuery request, CancellationToken cancellationToken);
#endif
    }

    public abstract class With<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : Response
    {
#if MEDIATR
        public abstract Task<Result<TResponse>> Handle(
            TQuery request,
            CancellationToken cancellationToken
        );
#endif
#if MEDIATOR
        public abstract ValueTask<Result<TResponse>> Handle(
            TQuery request,
            CancellationToken cancellationToken
        );
#endif

    }
}
