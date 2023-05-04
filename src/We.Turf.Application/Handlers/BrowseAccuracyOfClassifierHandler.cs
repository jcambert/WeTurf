using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

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

#if MEDIATOR
    public override async ValueTask<Result<BrowseAccuracyOfClassifierResponse>> Handle(
        BrowseAccuracyOfClassifierQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseAccuracyOfClassifierResponse>> Handle(
        BrowseAccuracyOfClassifierQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var res = await Repository.ToListAsync(cancellationToken);
        return new BrowseAccuracyOfClassifierResponse(MapToDtoList(res));
    }
}
