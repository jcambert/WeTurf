using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadResultatIntoDbQuery:IRequest<Result< LoadResultatIntoDbResponse>>
{
    string Filename { get; set; }
}
public sealed record LoadResultatIntoDbResponse(List<ResultatDto> Resultats):Response;