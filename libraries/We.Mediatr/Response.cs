#if MEDIATOR
using Mediator;
#endif
using We.Results;

namespace We.Mediatr;

public record Response
{
    public static implicit operator Result<Response>(Response response) => Result.Create(response);
#if MEDIATOR
    //public static implicit operator Unit(Response response) => new Unit();
#endif
}
