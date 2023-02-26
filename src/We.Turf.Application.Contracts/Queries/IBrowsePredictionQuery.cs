using System;
using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionQuery : IRequest<BrowsePredictionResponse>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowsePredictionResponse(List<PredictedDto> Predicteds) : BaseResponse;