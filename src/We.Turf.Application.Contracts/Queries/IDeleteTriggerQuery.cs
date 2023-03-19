using We.Mediatr;

namespace We.Turf.Queries;

public interface IDeleteTriggerQuery: IQuery< DeleteTriggerResponse>
{
    Guid Id { get; set; }   
}

public sealed record DeleteTriggerResponse():Response;
