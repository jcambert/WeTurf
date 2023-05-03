using We.Results;

namespace We.Turf.Handlers;

public class DeleteTriggerHandler : TriggerBaseHandler<DeleteTriggerQuery, DeleteTriggerResponse>
{
    public DeleteTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if MEDIATOR
    public override async ValueTask<Result< DeleteTriggerResponse>> Handle(DeleteTriggerQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result< DeleteTriggerResponse>> Handle(DeleteTriggerQuery request, CancellationToken cancellationToken)
#endif
    {

        await Repository.DeleteAsync(request.Id,true,cancellationToken);
        return new DeleteTriggerResponse();
    }
}
