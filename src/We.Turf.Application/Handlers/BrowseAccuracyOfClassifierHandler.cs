using MediatR;
using System.Linq;
using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseAccuracyOfClassifierHandler : BaseHandler<BrowseAccuracyOfClassifierQuery, BrowseAccuracyOfClassifierResponse>
{

    //IRequestHandler<BrowsePredictionPerClassifierQuery, BrowsePredictionPerClassifierResponse> Handler1 => GetRequiredService<IRequestHandler<BrowsePredictionPerClassifierQuery, BrowsePredictionPerClassifierResponse>>();
    public BrowseAccuracyOfClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<BrowseAccuracyOfClassifierResponse> Handle(BrowseAccuracyOfClassifierQuery request, CancellationToken cancellationToken)
    {
        var req1 = new BrowsePredictionPerClassifierQuery();
        var req2=new BrowseResultatPerClassifierQuery();

        var res1=await Mediator.Send(req1, cancellationToken);
        var res2=await Mediator.Send(req2, cancellationToken);

        var l1 = res1.PredictionPerClassifier;
        var l2 = res2.ResultatPerClassifiers;

        var v=l1.Join(l2,p=>p.Classifier,x=>x.Classifier,(p,x)=>new AccuracyOfClassifierDto (){ Classifier=p.Classifier,PredictionCount=p.Counting,ResultatCount=x.Counting });

        return new BrowseAccuracyOfClassifierResponse(v.ToList());
    }
}
