using We.AbpExtensions;
using We.Results    ;
using We.Turf.Entities;
namespace We.Turf.Handlers;

public class GetLastScrappedHandler : BaseHandler<GetLastScrappedQuery, GetLastScrappedResponse>
{
    IRepository<LastScrapped> repository=>GetRequiredService<IRepository<LastScrapped>>();
    public GetLastScrappedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< GetLastScrappedResponse>> Handle(GetLastScrappedQuery request, CancellationToken cancellationToken)
    {
        var result=await repository.FirstOrDefaultAsync();

        return result!=null? new GetLastScrappedResponse(ObjectMapper.Map<LastScrapped,LastScrappedDto>(result)): new GetLastScrappedResponse(null);
    }
}
