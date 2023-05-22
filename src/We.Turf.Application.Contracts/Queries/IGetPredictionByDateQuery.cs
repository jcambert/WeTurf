using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionByDateQuery : WeM.IQuery<BrowsePredictionByDateResponse>
{

    DateOnly? Date { get; set; }
    int? Reunion { get; set; }
    int? Course { get; set; }

}

public sealed record BrowsePredictionByDateResponse(List<PredictionByDateDto> Predictions) : WeM.Response;
