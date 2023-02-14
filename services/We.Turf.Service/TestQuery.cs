namespace We.Turf.Service;
public class TestScript : AnacondaExecuteStript
{
    public TestScript(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "test";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }
}
public class TestHandler : BaseCommandRequestHandler<TestQuery, TestResponse>
{
    public TestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override ValueTask<TestResponse> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        Logger?.LogTrace("Start Test");
        var test = ServiceProvider.GetRequiredService<TestScript>();
        
        test.Send(Python);
        Logger?.LogTrace("End Test");
        return ValueTask.FromResult(new TestResponse ()); ;
    }
}

public class TestQuery:IRequest<TestResponse>
{
}

public sealed record TestResponse();
