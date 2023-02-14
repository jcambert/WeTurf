using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace We.Turf.Service;

public class LoadPredictedIntoDbHandler : BaseRequestHandler<LoadPredictedIntoDbQuery, LoadPredictedIntoDbResponse>
{
    private readonly IHttpClientFactory _clientFactory;

    public LoadPredictedIntoDbHandler(IServiceProvider serviceProvider,IHttpClientFactory clientFactory) : base(serviceProvider)
    {
        this._clientFactory=clientFactory;
    }

    public override async ValueTask<LoadPredictedIntoDbResponse> Handle(LoadPredictedIntoDbQuery request, CancellationToken cancellationToken)
    {
        var httpClient = _clientFactory.CreateClient(HttpClientApi.NAME);
        
        //JsonContent jsonContent = JsonContent.Create(request,typeof(LoadPredictedIntoDbQuery));
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IgnoreReadOnlyProperties = true,
            NumberHandling = JsonNumberHandling.WriteAsString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var response=await httpClient.PostAsJsonAsync("api/app/pmu/load-predicted-into-db", request,options, cancellationToken);
        if(response.IsSuccessStatusCode) {
            return new LoadPredictedIntoDbResponse();
        }
        throw new ApplicationException();
    }
}
