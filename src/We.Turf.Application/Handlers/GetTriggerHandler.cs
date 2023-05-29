namespace We.Turf.Handlers;

public class GetTriggerHandler : TriggerBaseHandler<GetTriggerQuery, GetTriggerResponse>
{
    public GetTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<GetTriggerResponse>> InternalHandle(
        GetTriggerQuery request,
        CancellationToken cancellationToken
    )
    {
        var e = await Repository.GetAsync(request.Id, false, cancellationToken);
        return new GetTriggerResponse(ReverseMap(e));
    }
}
