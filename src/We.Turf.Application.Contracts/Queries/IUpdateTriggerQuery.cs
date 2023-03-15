using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IUpdateTriggerQuery:IRequest<Result<UpdateTriggerResponse>>
{
    Guid Id { get; set; }
    TimeOnly Start { get; set; }
}

public sealed record UpdateTriggerResponse(ScrapTriggerDto Trigger):Response;
