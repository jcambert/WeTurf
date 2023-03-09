using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface ISetThemeCommand : ICommand<SetThemeCommandResult>
{
    string Name { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ISetThemeCommand),IncludeSelf =true)]
public class SetThemeCommand: ISetThemeCommand
{
    public SetThemeCommand(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}

public sealed record SetThemeCommandResult();
