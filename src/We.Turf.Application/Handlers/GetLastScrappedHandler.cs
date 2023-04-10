using We.AbpExtensions;
using We.Results    ;
using We.Turf.Entities;
namespace We.Turf.Handlers;

public class GetLastScrappedHandler : AbpHandler.With<GetLastScrappedQuery, GetLastScrappedResponse, LastScrapped, LastScrappedDto>
{
    public GetLastScrappedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< GetLastScrappedResponse>> Handle(GetLastScrappedQuery request, CancellationToken cancellationToken)
    {
        var result=await Repository.FirstOrDefaultAsync();
        if(result == null)
        {
            return NotFound($"{nameof(LastScrapped)} didn't exists");
        }
        return new GetLastScrappedResponse(MapToDto(result));
    }
}
