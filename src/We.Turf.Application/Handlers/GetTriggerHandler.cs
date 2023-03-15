using We.Results;

namespace We.Turf.Handlers;

public class GetTriggerHandler : TriggerBaseHandler<GetTriggerQuery, GetTriggerResponse>
{
    public GetTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< GetTriggerResponse>> Handle(GetTriggerQuery request, CancellationToken cancellationToken)
    {
        var e=await Repository.GetAsync(request.Id);
        return new GetTriggerResponse(ReverseMap(e));
    }
}
