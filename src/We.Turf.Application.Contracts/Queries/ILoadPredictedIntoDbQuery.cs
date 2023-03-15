using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadPredictedIntoDbQuery:IRequest<Result< LoadPredictedIntoDbResponse>>
{
    string Filename { get; set; }   
}

public sealed record LoadPredictedIntoDbResponse(List<PredictedDto> Predicted):Response;
