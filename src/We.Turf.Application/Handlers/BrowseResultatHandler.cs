using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

internal class ResultatByDate : Specification<Resultat>
{
    public ResultatByDate(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowseResultatHandler
    : AbpHandler.With<BrowseResultatQuery, BrowseResultatResponse, Resultat, ResultatDto>
{
    public BrowseResultatHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }
#if MEDIATOR
    public override async ValueTask<Result<BrowseResultatResponse>> Handle(
        BrowseResultatQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseResultatResponse>> Handle(
        BrowseResultatQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        LogTrace($"{nameof(BrowseResultatHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ResultatByDate(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatResponse(MapToDtoList(result));
    }
}
