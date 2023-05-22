using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We.Results;

namespace We.Turf;

public class PmuStatAppService : TurfAppService, IPmuStatAppService
{
    IPmuServiceAppService PmuService =>
        LazyServiceProvider.LazyGetRequiredService<IPmuServiceAppService>();

    public async Task<Result<SommeDesMises>> GetSommeDesMise(
        DateOnly date,
        string? classifier = null,
        TypePari pari = TypePari.Tous
    )
    {
        var res0=await PmuService.BrowsePredictionBydate(new(){Date=date});
        int mise=res0?res0.Predictions.Count():0;
        if(pari==TypePari.Tous)
            mise*=2;

        var t = PmuService.GetStatisticsWithDate(
            new()
            {
                Start = date,
                Classifier = classifier,
                Pari = pari,
                IncludeNonArrive = true
            }
        );
        var (res, response, errors) = await t;
        if (res)
        {
            var stats = response.Stats;
            int mise = stats.Sum(x => x.Mise);
            double dividende = stats.Sum(x => x.Dividende);
            return Result.Create<SommeDesMises>(new SommeDesMises(mise, dividende));
        }
        else
        {
            return Result.Failure<SommeDesMises>(errors.ToArray());
        }
    }
}
