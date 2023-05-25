using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetMiseQuery : WeM.IQuery<GetMiseResponse>
{
    DateOnly Date { get; set; }
    string Classifier { get; set; }

    int? Reunion { get; set; }
    int? Course { get; set; }
}

public sealed record GetMiseResponse(List<MiseDto> Mises) : WeM.Response;
