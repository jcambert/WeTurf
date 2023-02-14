using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

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
        //JsonContent jsonContent = JsonContent.Create(request, typeof(LoadPredictedIntoDbQuery));
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IgnoreReadOnlyProperties = true,
            NumberHandling = JsonNumberHandling.WriteAsString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var response = await httpClient.PostAsJsonAsync("api/app/pmu/load-resultat-into-db", request,options, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return new LoadResultatIntoDbResponse();
        }
        throw new ApplicationException();
    }
}
