namespace We.Turf.Handlers;

public class BrowseAccuracyOfClassifierHandler
    : AbpHandler.With<
          BrowseAccuracyOfClassifierQuery,
          BrowseAccuracyOfClassifierResponse,
          AccuracyPerClassifier,
          AccuracyPerClassifierDto
      >
{
    public BrowseAccuracyOfClassifierHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<BrowseAccuracyOfClassifierResponse>> InternalHandle(
        BrowseAccuracyOfClassifierQuery request,
        CancellationToken cancellationToken
    )
    {
        var res = await Repository.ToListAsync(cancellationToken);
        return new BrowseAccuracyOfClassifierResponse(MapToDtoList(res));
    }
}
