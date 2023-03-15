using We.Results;

namespace We.Turf;

public interface IPmuTriggerAppService:IApplicationService
{
    Task<Result<GetTriggerResponse>> GetAsync(GetTriggerQuery query);

    Task<Result<CreateTriggerResponse>> CreateAsync(CreateTriggerQuery query);

    Task<Result<UpdateTriggerResponse>> UpdateAsync(UpdateTriggerQuery query);
    Task<Result<DeleteTriggerResponse>> DeleteAsync(DeleteTriggerQuery query);
}
