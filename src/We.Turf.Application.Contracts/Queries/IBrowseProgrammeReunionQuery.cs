using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseProgrammeReunionQuery : IQuery<BrowseProgrammeReunionResponse>
{
    DateOnly Date { get; set; }
}

public sealed record BrowseProgrammeReunionResponse(List<ProgrammeReunionDto> Reunions) : Response;
