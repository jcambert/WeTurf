using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.Mediatr;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface IApplyLanguageCommand : IQuery<ApplyLanguageResult>
{
    string CultureName { get; init; }
    string UiCultureName { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IApplyLanguageCommand), IncludeSelf = true)]
public class ApplyLanguageCommand : IApplyLanguageCommand
{
    public ApplyLanguageCommand(string cultureName, string uiCultureName)
    {
        this.CultureName = cultureName;
        this.UiCultureName = uiCultureName;
    }

    public string CultureName { get; init; }
    public string UiCultureName { get; init; }
}

public sealed record ApplyLanguageResult() : Response;
