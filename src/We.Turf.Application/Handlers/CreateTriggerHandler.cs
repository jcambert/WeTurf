namespace We.Turf.Handlers;

public class CreateTriggerHandler : TriggerBaseHandler<CreateTriggerQuery, CreateTriggerResponse>
{
    public CreateTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<CreateTriggerResponse>> InternalHandle(
        CreateTriggerQuery request,
        CancellationToken cancellationToken
    )
    {
        var e = new ScrapTrigger() { Start = request.Start };
        var res = await Repository.InsertAsync(e, true, cancellationToken);
        return new CreateTriggerResponse(ReverseMap(res));
    }
}
