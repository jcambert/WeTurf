namespace We.Turf.Entities;

public class ResultatOfPredictedCountByReunionCourseDto : EntityDto
{
    //"Date", "Reunion","Course",count("NumeroPmu")
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NombreDePrediction { get; set; }
    public string Classifier { get; set; }
}
