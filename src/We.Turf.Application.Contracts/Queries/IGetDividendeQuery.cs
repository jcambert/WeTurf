using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetDividendeQuery : WeM.IQuery<GetDividendeResponse>
{
    DateOnly Date { get; set; }
    int? Reunion { get; set; }
    int? Course { get; set; }
    string Classifier { get; set; }
}

public sealed record GetDividendeResponse(List<DividendeDto> Dividendes) : WeM.Response;
