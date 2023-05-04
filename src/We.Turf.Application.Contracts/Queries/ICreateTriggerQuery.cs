using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ICreateTriggerQuery : WeM.IQuery<CreateTriggerResponse>
{
    public TimeOnly Start { get; set; }
}

public sealed record CreateTriggerResponse(ScrapTriggerDto Trigger) : WeM.Response;
