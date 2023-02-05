using System.Threading.Tasks;
using We.Turf.Queries;

namespace We.Turf.Controllers;

public class PmuController : TurfController, IPmuServiceAppService
{
    protected readonly IPmuServiceAppService pmuService;
    public PmuController(IPmuServiceAppService service)
    => (pmuService) = (service);

    public Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query)
    =>pmuService.LoadToPredictIntoDatabase(query);
}
