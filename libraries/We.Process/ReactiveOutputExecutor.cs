using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using We.Results;

namespace We.Processes;

public class SchedulerProvider : ISchedulerProvider
{
    public IScheduler Scheduler { get; } = new EventLoopScheduler();
}

public interface ISchedulerProvider
{
    IScheduler Scheduler { get; }
}

public class ReactiveOutputExecutor : IReactiveOutputExecutor
{
    public ReactiveOutputExecutor() { }

    ISubject<string> _output = new Subject<string>();
    public IObservable<string> OnOutput => _output.AsObservable();

    public void Connect(IExecutor executor)
    {
        executor.OnOutput.Subscribe(output => _output.OnNext(output));
    }

    public void Write(string message) => _output.OnNext(message);

    public void Write(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Write($"{error.Failure}-{error.Message}");
        }
    }

    public void WriteLine(string message) => _output.OnNext($"{message}\n");

    public void WriteLine(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            WriteLine($"{error.Failure}-{error.Message}");
        }
    }
}

public interface IReactiveOutputExecutor
{
    void Connect(IExecutor executor);
    IObservable<string> OnOutput { get; }

    void Write(string message);

    void Write(IEnumerable<Error> errors);

    void WriteLine(string message);

    void WriteLine(IEnumerable<Error> errors);
}
