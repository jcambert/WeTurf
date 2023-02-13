namespace We.Turf.Service;

public class GetLastScrappingDateQuery : IRequest<GetLastScrappingDateResponse>
{
}

public sealed record GetLastScrappingDateResponse(DateOnly LastDate);