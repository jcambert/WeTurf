using We.Mediatr;

namespace We.Turf.Queries;

public interface IScrapQuery:IQuery<ScrapResponse>
{
    DateOnly Start { get; set; }
    DateOnly End { get; set; }
    bool DeleteFilesIfExists { get; set; }

}
public sealed record ScrapResponse():Response;