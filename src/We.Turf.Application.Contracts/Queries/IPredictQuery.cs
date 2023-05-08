namespace We.Turf.Queries;

public interface IPredictQuery : WeM.IQuery<PredictResponse>
{
    string? UseFolder { get; set; }
    bool DeleteFilesIfExists { get; set; }
}

public sealed record PredictResponse() : WeM.Response;
