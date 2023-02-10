using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatPerClassifierQuery:IRequest<BrowseResultatPerClassifierResponse>
{
}
public sealed record BrowseResultatPerClassifierResponse(List<ResultatPerClassifierDto> ResultatPerClassifiers);