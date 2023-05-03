namespace We.Turf.Entities;

[Serializable]
public class ProgrammeReunionDto : EntityDto
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public string? Hippodrome { get; set; }
    public string? Discipline { get; set; }
    public int Nombre { get; set; }
}
