namespace We.Turf.Service;

public interface ICommand
{
    void Send(TextWriter writer);
    void Send(IObserver<string> writer);
}
