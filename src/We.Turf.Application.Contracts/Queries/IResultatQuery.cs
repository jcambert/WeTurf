using We.Mediatr;

namespace We.Turf.Queries;

public interface IResultatQuery:IQuery<ResultatResponse>
{
     DateOnly Start { get; set; }
     DateOnly End { get; set; }
    bool DeleteFilesIfExists { get; set; }
}
public sealed record ResultatResponse():Response;