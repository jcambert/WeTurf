using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RequestLocalization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace We.Bootswatch.Components.Web.BasicTheme.Services;

public interface ILangProvider : ISelectorProvider<ILang>
{


}
public class LangProvider : SelectorProvider<ILang>, ILangProvider
{
    private IReadOnlyList<Lang> _values;
    private Lang _currentLanguage;

    ILanguageProvider LanguageProvider=>ServiceProvider.LazyGetRequiredService<ILanguageProvider>();
    IAbpRequestLocalizationOptionsProvider RequestLocalizationOptionsProvider => ServiceProvider.LazyGetRequiredService<IAbpRequestLocalizationOptionsProvider>();
    public LangProvider(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context, NavigationManager navigationManager) : base(serviceProvider, context, navigationManager)
    {
    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var languages = await LanguageProvider.GetLanguagesAsync();
        _values = languages.Select(x => new Lang(x)).ToList();
        await SetCurrentAsync();
    }

    private async Task SetCurrentAsync()
    {
        var currentLanguage = _values.Select(x=>x.Language).FindByCulture(
             CultureInfo.CurrentCulture.Name,
             CultureInfo.CurrentUICulture.Name
             );

        if (currentLanguage == null)
        {
            var localizationOptions = await RequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
            if (localizationOptions.DefaultRequestCulture != null)
            {
                currentLanguage = new LanguageInfo(
                    localizationOptions.DefaultRequestCulture.Culture.Name,
                    localizationOptions.DefaultRequestCulture.UICulture.Name,
                    localizationOptions.DefaultRequestCulture.UICulture.DisplayName);
            }
            else
            {
                currentLanguage = new LanguageInfo(
                    CultureInfo.CurrentCulture.Name,
                    CultureInfo.CurrentUICulture.Name,
                    CultureInfo.CurrentUICulture.DisplayName);
            }
        }

        _currentLanguage = new Lang( currentLanguage);
    }

    protected override ILang Default { get; }
    protected override List<ILang> Values => _values?.ToList<ILang>();
    protected override string CookieName { get; } = string.Empty;
    


    public override ILang GetCurrent() => _currentLanguage;
}
public interface ILang : INameable, IEquatable<ILang>, IEqualityComparer<ILang>
{
    string CultureName { get; }
    string UiCultureName { get;  }

    LanguageInfo LanguageInfo { get; }
}

public class Lang : ILang
{
    public string Name => this.Language.DisplayName;
    public LanguageInfo Language { get; init; }
    public string CultureName => Language.CultureName;
    public string UiCultureName =>Language.UiCultureName;
    public LanguageInfo LanguageInfo { get; }

    public Lang(LanguageInfo li)
    {
        this.Language = li;
    }
    public bool Equals(ILang other)
    {
        if (other == null) return false;
        return GetType() == other.GetType() && Name == other.Name;
    }

    public bool Equals(ILang x, ILang y)
     => x.Name == y.Name;

    public int GetHashCode([DisallowNull] ILang obj)
    => obj.Name.GetHashCode();
}