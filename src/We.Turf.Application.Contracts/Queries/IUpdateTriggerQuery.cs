using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IUpdateTriggerQuery : IQuery<UpdateTriggerResponse>
{
    Guid Id { get; set; }
    TimeOnly Start { get; set; }
}

public sealed record UpdateTriggerResponse(ScrapTriggerDto Trigger) : Response;
