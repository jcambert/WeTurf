using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;




public interface IApplyMenuStyleCommand : ICommand<ApplyMenuStyleResult>
{
    string Name { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IApplyMenuStyleCommand),IncludeSelf =true)]
public class ApplyMenuStyleCommand : IApplyMenuStyleCommand
{
    public ApplyMenuStyleCommand(string name)
    {
        this.Name = name;
    }
    public string Name { get; init; }
}
public sealed record ApplyMenuStyleResult();
