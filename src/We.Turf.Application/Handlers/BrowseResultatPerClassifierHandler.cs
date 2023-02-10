using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseResultatPerClassifierHandler : BaseHandler<BrowseResultatPerClassifierQuery, BrowseResultatPerClassifierResponse>
{
    IRepository<ResultatPerClassifier> repository => GetRequiredService<IRepository<ResultatPerClassifier>>();
    public BrowseResultatPerClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<BrowseResultatPerClassifierResponse> Handle(BrowseResultatPerClassifierQuery request, CancellationToken cancellationToken)
    {
        var query = await repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatPerClassifierResponse(ObjectMapper.Map<List<ResultatPerClassifier>, List<ResultatPerClassifierDto>>(result));
    }
}
