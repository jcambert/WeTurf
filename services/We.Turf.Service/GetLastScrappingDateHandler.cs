using System.Net.Http.Json;

namespace We.Turf.Service;

public class GetLastScrappingDateHandler : BaseRequestHandler<GetLastScrappingDateQuery, GetLastScrappingDateResponse>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GetLastScrappingDateHandler(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory) : base(serviceProvider)
    {
        _httpClientFactory = httpClientFactory;
    }

    public override async ValueTask<GetLastScrappingDateResponse> Handle(GetLastScrappingDateQuery request, CancellationToken cancellationToken)
    {
        //https://localhost:44381/api/app/last-scrapped
        //var res0=await _httpClient.GetStringAsync("api/app/last-scrapped");
        HttpClient _httpClient = _httpClientFactory.CreateClient(HttpClientApi.NAME);
        var res = await _httpClient.GetFromJsonAsync<LastScrappedHeader>("api/app/last-scrapped",cancellationToken);

        return new GetLastScrappingDateResponse(DateOnly.FromDateTime(res.LastScrapped.LastDate));
    }
}
