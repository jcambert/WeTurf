using System.Net;
using We.Mediatr;
using We.Results;

namespace We.Turf;

public class PmuTriggerAppService : TurfAppService, IPmuTriggerAppService
{
    public Task<Result<CreateTriggerResponse>> CreateAsync(CreateTriggerQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<DeleteTriggerResponse>> DeleteAsync(DeleteTriggerQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetTriggerResponse>> GetAsync(GetTriggerQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<UpdateTriggerResponse>> UpdateAsync(UpdateTriggerQuery query) =>
        Mediator.Send(query).AsTaskWrap();
}
