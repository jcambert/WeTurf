using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ICreateTriggerQuery:IQuery< CreateTriggerResponse>
{
    public TimeOnly Start { get; set; }
}

public sealed record CreateTriggerResponse(ScrapTriggerDto Trigger):Response;
