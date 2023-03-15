using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionQuery : IRequest<Result<BrowsePredictionResponse>>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowsePredictionResponse(List<PredictedDto> Predicteds) : Response;