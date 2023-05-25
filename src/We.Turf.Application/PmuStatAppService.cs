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
        int mise = 0;
        double dividende = 0.0;
        bool _all_classifier = classifier == TurfDomainConstants.ALL_CLASSIFIER;

        var res00 = await Mediator.Send(
            new GetMiseQuery() { Date = date, Classifier = classifier }
        );

        if (_all_classifier)
        {
            var t0 = PmuService.BrowsePredictionBydate(new() { Date = date });
            var (res0, resp0, errors0) = await t0;
            if (res0)
            {
                mise = resp0.Predictions.Count();
            }
            else
            {
                return Result.Failure<SommeDesMises>(
                    "Impossible de recuperer les predictions par date"
                );
            }
            if (pari == TypePari.Tous)
                mise *= 2;

            var t1 = PmuService.BrowseResultatOfPredictedWithoutClassifier(
                new()
                {
                    Date = date,
                    Pari = pari,
                    IncludeNonArrive = true
                }
            );
            var (res1, resp1, errors1) = await t1;
            if (res0)
            {
                dividende = resp1.Resultats.Sum(x => x?.Dividende ?? 0.0);
            }
            else
            {
                return Result.Failure<SommeDesMises>(
                    "Impossible de recuperer les resultats des predictions sans classificateur par date"
                );
            }
            return Result.Create(new SommeDesMises(mise, dividende));
        }
        else
        {
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
                mise = res00.Value.Mises.Sum(x => x.Somme);

                dividende = stats.Sum(x => x.Dividende);
                return Result.Create(new SommeDesMises(mise, dividende));
            }
            else
            {
                return Result.Failure<SommeDesMises>(errors.ToArray());
            }
        }
    }
}
