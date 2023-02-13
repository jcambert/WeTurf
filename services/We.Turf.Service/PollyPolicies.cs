using Polly.Extensions.Http;
using Polly;

namespace We.Turf.Service;

internal class PollyPolicies
{
    internal static IAsyncPolicy<HttpResponseMessage> RetryPolicy
    => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    

    internal static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy
    => HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    
}
