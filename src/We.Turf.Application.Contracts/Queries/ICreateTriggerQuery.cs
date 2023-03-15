using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ICreateTriggerQuery:IRequest<Result< CreateTriggerResponse>>
{
    public TimeOnly Start { get; set; }
}

public sealed record CreateTriggerResponse(ScrapTriggerDto Trigger):Response;
