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

        bool _pari_tous = pari == TypePari.Tous;
        var res00 = await Mediator.Send(
            new GetMiseQuery() { Date = date, Classifier = classifier }
        );
        if (res00)
            mise = res00.Value.Mises.Sum(x => (_pari_tous ? x.Somme * 2 : x.Somme));
        var res01 = await Mediator.Send(
            new GetDividendeQuery()
            {
                Date = date,
                Classifier = classifier,
                Pari = pari
            }
        );
        if (res01)
            dividende = res01.Value.Dividendes.Sum(x => x.Somme);
        return Result.Create(new SommeDesMises(mise, dividende));
    }
}
