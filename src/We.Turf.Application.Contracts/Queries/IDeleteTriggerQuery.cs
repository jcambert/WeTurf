using System;

namespace We.Turf.Queries;

public interface IDeleteTriggerQuery:IRequest<DeleteTriggerResponse>
{
    Guid Id { get; set; }   
}

public sealed record DeleteTriggerResponse();
