using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace We.Processes;

public static class ExecutorExtensions
{


    public static IServiceCollection UseExecutor(this IServiceCollection services)
    {
      
        services.AddSingleton<DriveService>();
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
            var anaconda= new Anaconda(di) { BasePath = o.AnacondBasePath, EnvironmentName = o.EnvironmentName };

            return anaconda;
        });
        services.AddTransient<IAnacondaActivationCommand, AnacondaActivationCommand>(sp=>new AnacondaActivationCommand(sp));
        services.AddTransient<IAnacondaDeactivationCommand, AnacondaDeactivationCommand>(sp=>new AnacondaDeactivationCommand(sp));
        services.AddTransient<IPythonExecutor, PythonExecutor>();
        services.AddScoped<IReactiveOutputExecutor, ReactiveOutputExecutor>();
        services.AddScoped<ISchedulerProvider, SchedulerProvider>();
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
