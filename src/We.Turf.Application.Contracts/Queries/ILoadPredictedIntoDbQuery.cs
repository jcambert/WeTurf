using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadPredictedIntoDbQuery:IRequest<LoadPredictedIntoDbResponse>
{
    string Filename { get; set; }   
}

public sealed record LoadPredictedIntoDbResponse(List<PredictedDto> Predicted):BaseResponse
{
    
}
