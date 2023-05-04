namespace We.Turf.Queries;

public interface IPredictQuery : WeM.IQuery<PredictResponse>
{
    bool DeleteFilesIfExists { get; set; }
}

public sealed record PredictResponse() : WeM.Response;
