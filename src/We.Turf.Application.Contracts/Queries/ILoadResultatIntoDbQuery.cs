using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadResultatIntoDbQuery : WeM.IQuery<LoadResultatIntoDbResponse>
{
    string Filename { get; set; }
    bool Rename { get; set; }
}

public sealed record LoadResultatIntoDbResponse(List<ResultatDto> Resultats) : WeM.Response;
