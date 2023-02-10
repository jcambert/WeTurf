namespace We.Turf.Handlers;

public class DeleteTriggerHandler : TriggerBaseHandler<DeleteTriggerQuery, DeleteTriggerResponse>
{
    public DeleteTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<DeleteTriggerResponse> Handle(DeleteTriggerQuery request, CancellationToken cancellationToken)
    {

        await Repository.DeleteAsync(request.Id,true,cancellationToken);
        return new DeleteTriggerResponse();
    }
}
