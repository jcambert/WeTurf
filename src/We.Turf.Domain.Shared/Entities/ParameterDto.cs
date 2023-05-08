namespace We.Turf.Entities;

public class ParameterDto : EntityDto<Guid>
{
    public string? BaseScrapFolder { get; set; }
    public string? InputScrapFolder { get; set; }
    public string? OutputScrapFolder { get; set; }
    public string? PredictionFilename { get; set; }
    public string? CourseFilename { get; set; }
    public bool ScrapInFolder { get; set; }
    public string? InputScrapFolderMonthPattern { get; set; }
}
