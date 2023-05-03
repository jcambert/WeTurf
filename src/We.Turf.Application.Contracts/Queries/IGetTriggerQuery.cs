using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetTriggerQuery : IQuery<GetTriggerResponse>
{
    Guid Id { get; }
}

public sealed record GetTriggerResponse(ScrapTriggerDto Trigger) : Response;
