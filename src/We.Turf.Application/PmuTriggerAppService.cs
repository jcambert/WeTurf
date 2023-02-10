namespace We.Turf;

public class PmuTriggerAppService : TurfAppService, IPmuTriggerAppService
{
    public Task<CreateTriggerResponse> CreateAsync(CreateTriggerQuery query)
    => Mediator.Send(query);

    public Task<DeleteTriggerResponse> DeleteAsync(DeleteTriggerQuery query)
    => Mediator.Send(query);


    public Task<GetTriggerResponse> GetAsync(GetTriggerQuery query)
    => Mediator.Send(query);

    public Task<UpdateTriggerResponse> UpdateAsync(UpdateTriggerQuery query)
        => Mediator.Send(query);
}
