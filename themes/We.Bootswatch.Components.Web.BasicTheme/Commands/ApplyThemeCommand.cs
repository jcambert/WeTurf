using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface IApplyThemeCommand : ICommand<ApplyThemeResult>
{
    string Name { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IApplyThemeCommand),IncludeSelf =true)]
public class ApplyThemeCommand : IApplyThemeCommand
{
    public ApplyThemeCommand(string name)
    {
        this.Name = name;
    }
    public string Name { get; init; }
}
public sealed record ApplyThemeResult();