using Microsoft.Extensions.DependencyInjection;
namespace We.Processes;

public abstract class AnacondaCommand : BaseCommand, IAnacondaCommand
{
    protected IAnaconda Conda { get; init; }

    public AnacondaCommand(IAnaconda conda) => (Conda) = (conda);
    public AnacondaCommand(IServiceProvider serviceProvider) : this(serviceProvider.GetRequiredService<IAnaconda>())
    { }
}
