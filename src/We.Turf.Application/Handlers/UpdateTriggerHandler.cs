namespace We.Turf.Handlers;

public class UpdateTriggerHandler : TriggerBaseHandler<UpdateTriggerQuery, UpdateTriggerResponse>
{
    public UpdateTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<UpdateTriggerResponse>> InternalHandle(
        UpdateTriggerQuery request,
        CancellationToken cancellationToken
    )
    {
        var e = await Repository.GetAsync(request.Id, false, cancellationToken);
        Map(new ScrapTriggerDto() { Start = request.Start }, e);
        await Repository.UpdateAsync(e, true, cancellationToken);
        return new UpdateTriggerResponse(ReverseMap(e));
    }
}
