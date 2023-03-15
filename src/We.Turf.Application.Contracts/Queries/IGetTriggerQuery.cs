using System;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetTriggerQuery:IRequest<Result<GetTriggerResponse>>
{
    Guid Id { get; }
}

public sealed record GetTriggerResponse(ScrapTriggerDto Trigger):Response;
