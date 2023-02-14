namespace We.Turf.Service;

public interface IWinCommand : IObserver<string>, IDisposable
{
    //void Start();
    //Task ExecuteAsync(CancellationToken cancellationToken, params string[] args);
    //void End();
    //void Initialize();
    IObservable<string> OnOutput { get; }
}
