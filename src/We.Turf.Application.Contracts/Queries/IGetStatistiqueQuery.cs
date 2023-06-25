using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetStatistiqueQuery : WeM.IQuery<GetStatistiqueResponse> {
    string Classifier { get; set; }
    TypePari Pari { get; set; }
}

public sealed record GetStatistiqueResponse(List<StatDto> Stats) : WeM.Response;

public interface IGetStatistiqueWithDateQuery : WeM.IQuery<GetStatistiqueWithDateResponse>
{
    DateOnly? Start { get; set; }
    DateOnly? End { get; set; }
    string Classifier { get; set; }
    TypePari Pari { get; set; }

    bool IncludeNonArrive { get; set; }
}

public sealed record GetStatistiqueWithDateResponse(List<StatByDateDto> Stats) : WeM.Response;
