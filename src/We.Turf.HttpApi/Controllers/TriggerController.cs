using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Turf.Queries;

namespace We.Turf.Controllers;

public class TriggerController : TurfController, IPmuTriggerAppService
{
    protected readonly IPmuTriggerAppService pmuService;
    public TriggerController(IPmuTriggerAppService service)
    => (pmuService) = (service);

    [HttpPost]
    public Task<CreateTriggerResponse> CreateAsync( CreateTriggerQuery query)
    =>pmuService.CreateAsync(query);

    [HttpDelete]
    public Task<DeleteTriggerResponse> DeleteAsync(DeleteTriggerQuery query)
    =>pmuService.DeleteAsync(query);

    [HttpGet]
    public Task<GetTriggerResponse> GetAsync(GetTriggerQuery query)
    =>pmuService.GetAsync(query);

    [HttpPut]
    public Task<UpdateTriggerResponse> UpdateAsync([FromBody] UpdateTriggerQuery query)
    =>pmuService.UpdateAsync(query);
}
