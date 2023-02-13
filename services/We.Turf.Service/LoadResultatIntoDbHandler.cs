using System.Net.Http.Json;

namespace We.Turf.Service;

public class LoadResultatIntoDbHandler : BaseRequestHandler<LoadResultatIntoDbQuery, LoadResultatIntoDbResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    public LoadResultatIntoDbHandler(IServiceProvider serviceProvider, IHttpClientFactory clientFactory) : base(serviceProvider)
    {
        this._clientFactory = clientFactory;
    }

    public override async ValueTask<LoadResultatIntoDbResponse> Handle(LoadResultatIntoDbQuery request, CancellationToken cancellationToken)
    {
        var httpClient = _clientFactory.CreateClient(HttpClientApi.NAME);
        JsonContent jsonContent = JsonContent.Create(request, typeof(LoadPredictedIntoDbQuery));
        var response = await httpClient.PostAsJsonAsync("api/app/pmu/load-resultat-into-db", jsonContent, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return new LoadResultatIntoDbResponse();
        }
        throw new ApplicationException();
    }
}
