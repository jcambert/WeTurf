using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadResultatIntoDbQuery:IRequest<LoadResultatIntoDbResponse>
{
    string Filename { get; set; }
}
public sealed record LoadResultatIntoDbResponse(List<ResultatDto> Resultats);