using System.Globalization;
using Volo.Abp.Localization;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class LanguageSwitch
{
    private IReadOnlyList<LanguageInfo> _otherLanguages = new List<LanguageInfo>();
    private LanguageInfo? _currentLanguage = null;

    public LanguageSwitch() { }

    protected override async Task OnInitializedAsync()
    {
        var languages = await LanguageProvider.GetLanguagesAsync();
        var currentLanguage = languages.FindByCulture(
            CultureInfo.CurrentCulture.Name,
            CultureInfo.CurrentUICulture.Name
        );

        if (currentLanguage == null)
        {
            var localizationOptions =
                await RequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
            if (localizationOptions.DefaultRequestCulture != null)
            {
                currentLanguage = new LanguageInfo(
                    localizationOptions.DefaultRequestCulture.Culture.Name,
                    localizationOptions.DefaultRequestCulture.UICulture.Name,
                    localizationOptions.DefaultRequestCulture.UICulture.DisplayName
                );
            }
            else
            {
                currentLanguage = new LanguageInfo(
                    CultureInfo.CurrentCulture.Name,
                    CultureInfo.CurrentUICulture.Name,
                    CultureInfo.CurrentUICulture.DisplayName
                );
            }
        }

        _currentLanguage = currentLanguage;
        _otherLanguages = languages.Where(l => l != _currentLanguage).ToImmutableList();
    }

    private void ChangeLanguage(LanguageInfo language)
    {
        var relativeUrl = NavigationManager.Uri
            .RemovePreFix(NavigationManager.BaseUri)
            .EnsureStartsWith('/')
            .EnsureStartsWith('~');

        NavigationManager.NavigateTo(
            $"Abp/Languages/Switch?culture={language.CultureName}&uiCulture={language.UiCultureName}&returnUrl={relativeUrl}",
            forceLoad: true
        );
    }
}
