namespace We.Turf.Queries;

public interface IDeleteTriggerQuery : WeM.IQuery<DeleteTriggerResponse>
{
    Guid Id { get; set; }
}

public sealed record DeleteTriggerResponse() : WeM.Response;
