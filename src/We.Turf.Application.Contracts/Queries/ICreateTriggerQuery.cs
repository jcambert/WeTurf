using System;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ICreateTriggerQuery:IRequest<CreateTriggerResponse>
{
    public TimeOnly Start { get; set; }
}

public sealed record CreateTriggerResponse(ScrapTriggerDto Trigger);
