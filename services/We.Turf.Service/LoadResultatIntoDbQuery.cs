namespace We.Turf.Service;

public class LoadResultatIntoDbQuery:IRequest<LoadResultatIntoDbResponse>
{
    public string Filename { get; set; }
}
public sealed record LoadResultatIntoDbResponse();