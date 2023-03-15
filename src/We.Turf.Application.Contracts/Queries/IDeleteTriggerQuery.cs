using System;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Queries;

public interface IDeleteTriggerQuery:IRequest<Result< DeleteTriggerResponse>>
{
    Guid Id { get; set; }   
}

public sealed record DeleteTriggerResponse():Response;
