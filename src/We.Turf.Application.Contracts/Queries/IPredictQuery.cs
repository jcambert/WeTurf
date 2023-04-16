using We.Mediatr;

namespace We.Turf.Queries;

public interface IPredictQuery:IQuery<PredictResponse>
{
    bool DeleteFilesIfExists { get; set; }
}
public sealed record PredictResponse():Response;
