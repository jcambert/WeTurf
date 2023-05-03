using We.Results;

namespace We.Turf.Handlers;

public class GetTriggerHandler : TriggerBaseHandler<GetTriggerQuery, GetTriggerResponse>
{
    public GetTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if MEDIATOR
    public override async ValueTask<Result< GetTriggerResponse>> Handle(GetTriggerQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result< GetTriggerResponse>> Handle(GetTriggerQuery request, CancellationToken cancellationToken)
#endif
    {
        var e=await Repository.GetAsync(request.Id,false,cancellationToken);
        return new GetTriggerResponse(ReverseMap(e));
    }
}
