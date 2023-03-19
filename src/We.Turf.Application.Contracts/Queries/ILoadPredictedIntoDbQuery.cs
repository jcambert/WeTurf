using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadPredictedIntoDbQuery:IQuery< LoadPredictedIntoDbResponse>
{
    string Filename { get; set; }   
}

public sealed record LoadPredictedIntoDbResponse(List<PredictedDto> Predicted):Response;
