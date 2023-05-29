namespace We.Turf.Handlers;

public class GetLastScrappedHandler
    : AbpHandler.With<GetLastScrappedQuery, GetLastScrappedResponse, LastScrapped, LastScrappedDto>
{
    public GetLastScrappedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected override async Task<Result<GetLastScrappedResponse>> InternalHandle(
        GetLastScrappedQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(LastScrapped)} didn't exists");
        }
        return new GetLastScrappedResponse(MapToDto(result));
    }
}
