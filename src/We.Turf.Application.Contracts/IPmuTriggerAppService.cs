using System;
using We.Turf.Entities;

namespace We.Turf;

public interface IPmuTriggerAppService:IApplicationService
{
    Task<GetTriggerResponse> GetAsync(GetTriggerQuery query);

    Task<CreateTriggerResponse> CreateAsync(CreateTriggerQuery query);

    Task<UpdateTriggerResponse> UpdateAsync(UpdateTriggerQuery query);
    Task<DeleteTriggerResponse> DeleteAsync(DeleteTriggerQuery query);
}
