using System;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetTriggerQuery:IRequest<GetTriggerResponse>
{
    Guid Id { get; }
}

public sealed record GetTriggerResponse(ScrapTriggerDto Trigger);
