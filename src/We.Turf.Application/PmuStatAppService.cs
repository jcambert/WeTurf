using We.Mediatr;
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
        //int mise = 0;
        //double dividende = 0.0;
        bool _all_classifier = classifier == TurfDomainConstants.ALL_CLASSIFIER;

        if (_all_classifier)
        {
            var r0 = await Result
                .Create(new BrowsePredictionByDateQuery() { Date = date })
                .Bind(c => PmuService.BrowsePredictionBydate(c))
                .Match(
                    resp =>
                    {
                        var mise =
                            (pari == TypePari.Tous)
                                ? resp.Predictions.Count() * 2
                                : resp.Predictions.Count();
                        return Result.Success(new SommeDesMises(mise, 0));
                    },
                    fail =>
                        Result.Failure<SommeDesMises>(
                            "Impossible de recuperer les predictions par date"
                        )
                );
            /*
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
            */
            if (!r0)
                return r0;
            var r1 = await Result
                .Create(
                    new BrowseResultatOfPredictedWithoutClassifierQuery()
                    {
                        Date = date,
                        Pari = pari,
                        IncludeNonArrive = true
                    }
                )
                .Bind(c => PmuService.BrowseResultatOfPredictedWithoutClassifier(c))
                .Match(
                    resp =>
                    {
                        var dividende = resp.Resultats.Sum(x => x?.Dividende ?? 0.0);
                        return Result.Success(new SommeDesMises(0, dividende));
                    },
                    fail =>
                        Result.Failure<SommeDesMises>(
                            "Impossible de recuperer les resultats des predictions sans classificateur par date"
                        )
                );
            if (!r1)
                return r1;
            return Result.Success(new SommeDesMises(r0.Value.Mise, r1.Value.Dividende));
            /*var t1 = PmuService.BrowseResultatOfPredictedWithoutClassifier(
                new()
                {
                    Date = date,
                    Pari = pari,
                    IncludeNonArrive = true
                }
            );
            var (res1, resp1, errors1) = await t1;
            if (res1)
            {
                dividende = resp1.Resultats.Sum(x => x?.Dividende ?? 0.0);
            }
            else
            {
                return Result.Failure<SommeDesMises>(
                    "Impossible de recuperer les resultats des predictions sans classificateur par date"
                );
            }
            return Result.Create(new SommeDesMises(mise, dividende));*/
        }
        else
        {
            var r0 = await Result
                .Create(new GetMiseQuery() { Date = date, Classifier = classifier })
                .Bind(c => Mediator.Send(c).AsTaskWrap());
            if (!r0)
                return Result.Failure<SommeDesMises>("Impossible de recuperer les mises");

            /* var res00 = await Mediator.Send(
                 new GetMiseQuery() { Date = date, Classifier = classifier }
             );*/
            return await Result
                .Create(
                    new GetStatistiqueWithDateQuery()
                    {
                        Start = date,
                        Classifier = classifier,
                        Pari = pari,
                        IncludeNonArrive = true
                    }
                )
                .Bind(c => PmuService.GetStatisticsWithDate(c))
                .Match(
                    resp =>
                    {
                        var stats = resp.Stats;
                        int mise = r0.Value.Mises.Sum(x => x.Somme);

                        double dividende = stats.Sum(x => x.Dividende);
                        return Result.Create(new SommeDesMises(mise, dividende));
                    },
                    fail => Result.Failure<SommeDesMises>(fail.Errors.ToArray())
                );
            /*var (res, response, errors) = await t;
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
            }*/
        }
    }
}
