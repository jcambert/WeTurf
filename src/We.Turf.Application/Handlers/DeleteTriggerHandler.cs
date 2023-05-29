namespace We.Turf.Handlers;

public class DeleteTriggerHandler : TriggerBaseHandler<DeleteTriggerQuery, DeleteTriggerResponse>
{
    public DeleteTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<DeleteTriggerResponse>> InternalHandle(
        DeleteTriggerQuery request,
        CancellationToken cancellationToken
    )
    {
        await Repository.DeleteAsync(request.Id, true, cancellationToken);
        return new DeleteTriggerResponse();
    }
}
