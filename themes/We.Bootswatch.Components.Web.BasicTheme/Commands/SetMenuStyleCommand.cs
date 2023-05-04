using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.Mediatr;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface ISetMenuStyleCommand : IQuery<SetMenuStyleResult>
{
    string Style { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ISetMenuStyleCommand), IncludeSelf = true)]
public class SetMenuStyleCommand : ISetMenuStyleCommand
{
    public SetMenuStyleCommand(string style)
    {
        Style = style;
    }

    public string Style { get; init; }
}

public sealed record SetMenuStyleResult() : Response;
