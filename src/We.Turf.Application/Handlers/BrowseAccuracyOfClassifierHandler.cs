using MediatR;
using System.Linq;
using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseAccuracyOfClassifierHandler : BaseHandler<BrowseAccuracyOfClassifierQuery, BrowseAccuracyOfClassifierResponse>
{
    IRepository<AccuracyPerClassifier> repository => GetRequiredService<IRepository<AccuracyPerClassifier>>();

    //IRequestHandler<BrowsePredictionPerClassifierQuery, BrowsePredictionPerClassifierResponse> Handler1 => GetRequiredService<IRequestHandler<BrowsePredictionPerClassifierQuery, BrowsePredictionPerClassifierResponse>>();
    public BrowseAccuracyOfClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<BrowseAccuracyOfClassifierResponse> Handle(BrowseAccuracyOfClassifierQuery request, CancellationToken cancellationToken)
    {
        /*var req1 = new BrowsePredictionPerClassifierQuery();
        var req2=new BrowseResultatPerClassifierQuery();

        var res1=await Mediator.Send(req1, cancellationToken);
        var res2=await Mediator.Send(req2, cancellationToken);

        var l1 = res1.PredictionPerClassifier;
        var l2 = res2.ResultatPerClassifiers;
        l1=l1.Where(x=>l2.Any(y=>y.Classifier==x.Classifier && y.Date==x.Date)).ToList(); ;


        var v=l1.Join(l2,p=>p.Classifier,x=>x.Classifier,(p,x)=>new AccuracyPerClassifierDto (){ Classifier=p.Classifier,PredictionCount=p.Counting,ResultatCount=x.Counting });
        v=v.GroupBy(x => x.Classifier).Select(x => new AccuracyPerClassifierDto() { Classifier = x.Key, PredictionCount = x.Sum(y => y.PredictionCount), ResultatCount = x.Sum(y => y.ResultatCount) });*/
        var res = await repository.ToListAsync();
        return new BrowseAccuracyOfClassifierResponse(ObjectMapper.Map<List<AccuracyPerClassifier>,List<AccuracyPerClassifierDto>>(res));
    }
}
