using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class GetLastScrappedHandler
    : AbpHandler.With<GetLastScrappedQuery, GetLastScrappedResponse, LastScrapped, LastScrappedDto>
{
    public GetLastScrappedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }
#if MEDIATOR
    public override async ValueTask<Result<GetLastScrappedResponse>> Handle(
        GetLastScrappedQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<GetLastScrappedResponse>> Handle(
        GetLastScrappedQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(LastScrapped)} didn't exists");
        }
        return new GetLastScrappedResponse(MapToDto(result));
    }
}
