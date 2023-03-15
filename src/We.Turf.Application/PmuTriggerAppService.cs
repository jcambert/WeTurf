using We.Results;

namespace We.Turf;

public class PmuTriggerAppService : TurfAppService, IPmuTriggerAppService
{
    public Task<Result< CreateTriggerResponse>> CreateAsync(CreateTriggerQuery query)
    => Mediator.Send(query);

    public Task<Result<DeleteTriggerResponse>> DeleteAsync(DeleteTriggerQuery query)
    => Mediator.Send(query);


    public Task<Result<GetTriggerResponse>> GetAsync(GetTriggerQuery query)
    => Mediator.Send(query);

    public Task<Result<UpdateTriggerResponse>> UpdateAsync(UpdateTriggerQuery query)
        => Mediator.Send(query);
}
