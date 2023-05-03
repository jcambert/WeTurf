namespace We.Turf.Entities;

public class CourseDto : EntityDto<Guid>
{
    
    public int Reunion { get; set; }
    
    public int Numero { get; set; }
    
    public string? Libelle { get; set; }
    public string? LibelleCourt { get; set; }
    public int MontantPrix { get; set; }
    public int Distance { get; set; }
    public string? DistanceUnite { get; set; }
    public string? Discipline { get; set; }
    public string? Specialite { get; set; }
    public int NombrePartants { get; set; }
    public string? OrdreArrivee { get; set; }
    
    public DateOnly Date { get; set; }


}
