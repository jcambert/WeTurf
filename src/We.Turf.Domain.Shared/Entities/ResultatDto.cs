namespace We.Turf.Entities;

[Serializable]
public class ResultatDto : EntityDto<Guid>
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public string? Pari { get; set; }
    public int NumeroPmu { get; set; }
    public double Rapport { get; set; }
}
