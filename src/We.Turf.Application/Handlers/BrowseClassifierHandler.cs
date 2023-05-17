using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseClassifierHandler
    : AbpHandler.With<BrowseClassifierQuery, BrowseClassifierResponse, Classifier, ClassifierDto>
{
    public BrowseClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

#if MEDIATOR
    public override async ValueTask<Result<BrowseClassifierResponse>> Handle(
        BrowseClassifierQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseClassifierResponse>> Handle(
        BrowseClassifierQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var res = await Repository.ToListAsync(cancellationToken);
        return new BrowseClassifierResponse(MapToDtoList(res));
    }
}
