using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class UpdateTriggerHandler : TriggerBaseHandler<UpdateTriggerQuery, UpdateTriggerResponse>
{
    public UpdateTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<UpdateTriggerResponse>> Handle(UpdateTriggerQuery request, CancellationToken cancellationToken)
    {
        var e= await Repository.GetAsync(request.Id);
        Map(new ScrapTriggerDto() { Start=request.Start}, e);
        await Repository.UpdateAsync(e,true,cancellationToken);
        return new UpdateTriggerResponse(ReverseMap(e));
    }
}
