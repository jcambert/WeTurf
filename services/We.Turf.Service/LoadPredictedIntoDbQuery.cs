namespace We.Turf.Service;

[Serializable]
public class LoadPredictedIntoDbQuery : IRequest<LoadPredictedIntoDbResponse>
{
    public string Filename { get; set; } //= @"E:\projets\pmu_scrapper\output\predicted.csv";
}

public sealed record LoadPredictedIntoDbResponse();