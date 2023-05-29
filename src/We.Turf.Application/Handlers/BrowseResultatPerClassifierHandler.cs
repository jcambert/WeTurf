namespace We.Turf.Handlers;

public class BrowseResultatPerClassifierHandler
    : AbpHandler.With<
          BrowseResultatPerClassifierQuery,
          BrowseResultatPerClassifierResponse,
          ResultatPerClassifier,
          ResultatPerClassifierDto
      >
{
    public BrowseResultatPerClassifierHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<BrowseResultatPerClassifierResponse>> InternalHandle(
        BrowseResultatPerClassifierQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await Repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatPerClassifierResponse(MapToDtoList(result));
    }
}
