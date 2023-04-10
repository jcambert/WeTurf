using We.Mediatr;

namespace We.Turf.Queries;

public interface IScrapQuery:IQuery<ScrapResponse>
{
    DateOnly Start { get; set; }
    DateOnly End { get; set; }
}
public sealed record ScrapResponse():Response;