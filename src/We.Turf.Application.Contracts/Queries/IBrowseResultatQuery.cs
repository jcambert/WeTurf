using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatQuery : WeM.IQuery<BrowseResultatResponse>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowseResultatResponse(List<ResultatDto> Resultats) : WeM.Response;
