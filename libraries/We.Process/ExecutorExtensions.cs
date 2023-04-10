using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace We.Processes;

public static class ExecutorExtensions
{


    public static IServiceCollection UseExecutor(this IServiceCollection services)
    {
      
        services.AddSingleton<DriveInfo>();
        return services;
    }
    public static IServiceCollection UsePythonExecutor(this IServiceCollection services, Action<PythonExecutorOptions> opt)
    {
        services.UseExecutor();
        services.Configure(opt);
        services.AddTransient<IAnaconda, Anaconda>(sp =>
        {
            var di = sp.GetRequiredService<DriveService>();
            var o = sp.GetRequiredService<IOptions<PythonExecutorOptions>>().Value;
            return new Anaconda(di) { BasePath = o.AnacondBasePath, EnvironmentName = o.EnvironmentName };
        });
        services.AddTransient<IPythonExecutor, PythonExecutor>();
        return services;

    }
    public static IServiceCollection UseCommandExecutor(this IServiceCollection services, Action<ExecutorOptions> opt)
    {
        services.UseExecutor();
        services.Configure(opt);
        services.AddTransient<ICommandExecutor, CommandExecutor>();
        return services;
    }

}
