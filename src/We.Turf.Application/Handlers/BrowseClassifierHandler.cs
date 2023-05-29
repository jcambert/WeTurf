namespace We.Turf.Handlers;

public class BrowseClassifierHandler
    : AbpHandler.With<BrowseClassifierQuery, BrowseClassifierResponse, Classifier, ClassifierDto>
{
    public BrowseClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected override async Task<Result<BrowseClassifierResponse>> InternalHandle(
        BrowseClassifierQuery request,
        CancellationToken cancellationToken
    )
    {
        var res = await Repository.ToListAsync(cancellationToken);
        return new BrowseClassifierResponse(MapToDtoList(res));
    }
}
