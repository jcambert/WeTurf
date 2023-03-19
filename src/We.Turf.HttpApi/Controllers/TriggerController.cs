/*using Microsoft.AspNetCore.Mvc;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Controllers;

//[ApiController]
//[Area("pmu")]
//[Route("trigger/[action]")]

public class TriggerController : TurfController, IPmuTriggerAppService
{
    protected readonly IPmuTriggerAppService pmuService;
    public TriggerController(IPmuTriggerAppService service)
    => (pmuService) = (service);

    [HttpPost]
    public Task<Result<CreateTriggerResponse>> CreateAsync( CreateTriggerQuery query)
    =>pmuService.CreateAsync(query);

    [HttpDelete]
    public Task<Result<DeleteTriggerResponse>> DeleteAsync(DeleteTriggerQuery query)
    =>pmuService.DeleteAsync(query);

    [HttpGet]
    public Task<Result<GetTriggerResponse>> GetAsync(GetTriggerQuery query)
    =>pmuService.GetAsync(query);

    [HttpPut]
    public Task<Result<UpdateTriggerResponse>> UpdateAsync([FromBody] UpdateTriggerQuery query)
    =>pmuService.UpdateAsync(query);
}
*/