using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadPredictedIntoDbQuery : WeM.IQuery<LoadPredictedIntoDbResponse>
{
    string Filename { get; set; }
    bool Rename { get; set; }

    bool HasHeader { get; set; }

    char Separator { get; set; }
}

public sealed record LoadPredictedIntoDbResponse(List<PredictedDto> Predicted) : WeM.Response;
