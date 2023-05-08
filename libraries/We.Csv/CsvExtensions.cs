using Microsoft.Extensions.Logging;
using We.Csv;

namespace Microsoft.Extensions.DependencyInjection;

public static class CsvExtensions
{
    public static IServiceCollection UseCsvReader<T>(this IServiceCollection services)
        where T : class, new()
    {
        services.AddTransient<ICsvReader<T>, Reader<T>>(
            sp =>
            {
                return new Reader<T>(sp.GetService<ILogger<Reader<T>>>());
            }
        );
        return services;
    }
}
