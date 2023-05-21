using We.Results;

namespace We.Turf;

public sealed record SommeDesMises(int Mise, double Dividende);

public interface IPmuStatAppService : IApplicationService
{
    Task<Result<SommeDesMises>> GetSommeDesMise(
        DateOnly date,
        string classifier = null,
        TypePari pari = TypePari.Tous
    );
}
