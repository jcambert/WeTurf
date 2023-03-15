using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class CreateTriggerHandler : TriggerBaseHandler<CreateTriggerQuery, CreateTriggerResponse>
{
    public CreateTriggerHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< CreateTriggerResponse>> Handle(CreateTriggerQuery request, CancellationToken cancellationToken)
    {
        var e=new ScrapTrigger() { Start = request.Start };
        var res=await Repository.InsertAsync(e,true, cancellationToken);
        return new CreateTriggerResponse(ReverseMap(res));
    }
}
