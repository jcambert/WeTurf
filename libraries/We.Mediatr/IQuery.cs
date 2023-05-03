#if MEDIATR
using MediatR;
#endif
#if MEDIATOR
using Mediator;
#endif

using We.Results;

namespace We.Mediatr;

#if MEDIATR
public interface IQuery : IRequest { }

public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : Response { }
#endif
#if MEDIATOR
public interface IQuery : IRequest { }

public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : Response { }
#endif
