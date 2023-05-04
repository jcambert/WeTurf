using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetTriggerQuery : WeM.IQuery<GetTriggerResponse>
{
    Guid Id { get; }
}

public sealed record GetTriggerResponse(ScrapTriggerDto Trigger) : WeM.Response;
