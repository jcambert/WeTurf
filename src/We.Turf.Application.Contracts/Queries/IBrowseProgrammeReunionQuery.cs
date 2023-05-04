using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseProgrammeReunionQuery : WeM.IQuery<BrowseProgrammeReunionResponse>
{
    DateOnly Date { get; set; }
}

public sealed record BrowseProgrammeReunionResponse(List<ProgrammeReunionDto> Reunions)
    : WeM.Response;
