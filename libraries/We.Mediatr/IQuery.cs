using MediatR;
using We.Results;
namespace We.Mediatr;
public interface IQuery : IRequest
{

}
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    where TResponse : Response
{
}


