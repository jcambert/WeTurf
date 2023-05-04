namespace We.Turf.Queries;

public interface IScrapQuery : WeM.IQuery<ScrapResponse>
{
    DateOnly Start { get; set; }
    DateOnly End { get; set; }
    bool DeleteFilesIfExists { get; set; }
}

public sealed record ScrapResponse() : WeM.Response;
