namespace We.Turf.Entities;

[Serializable]
public class ScrapTriggerDto : EntityDto<Guid>
{
    public TimeOnly Start { get; set; }
}
