using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class UpdateTriggerHandler : TriggerBaseHandler<UpdateTriggerQuery, UpdateTriggerResponse>
{
    public UpdateTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<UpdateTriggerResponse>> Handle(
        UpdateTriggerQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<UpdateTriggerResponse>> Handle(
        UpdateTriggerQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var e = await Repository.GetAsync(request.Id, false, cancellationToken);
        Map(new ScrapTriggerDto() { Start = request.Start }, e);
        await Repository.UpdateAsync(e, true, cancellationToken);
        return new UpdateTriggerResponse(ReverseMap(e));
    }
}
