namespace We.Turf.Service;

public interface IExecutor<TCommand>
    where TCommand:ICommand
{
    IObservable<string> OnOutput { get; }
    Task Execute(CancellationToken stoppingToken);
}
