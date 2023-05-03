using System.Reactive.Concurrency;
using We.Results;

namespace We.Processes;

public interface IExecutor
{
    IObservable<string> OnOutput { get; }
    Task<Result> Execute(CancellationToken stoppingToken = default, params ICommand[] commands);
    Task<Result> SendAsync(string cmd, CancellationToken cancellationToken = default);
}
