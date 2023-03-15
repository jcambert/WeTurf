using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatPerClassifierQuery:IRequest<Result< BrowseResultatPerClassifierResponse>>
{
}
public sealed record BrowseResultatPerClassifierResponse(List<ResultatPerClassifierDto> ResultatPerClassifiers):Response;