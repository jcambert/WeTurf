using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class BootswatchConsts
{
    internal const string ThemeCookie = "theme";
    internal const string MenuStyleCookie = "menu_style";
    internal const string DefaultTheme = "Darkly";

    //public static IReadOnlyList<string> SupportedThemes = new List<string>() { "Darkly", "Cerulean", "Litera", "Materia","Pulse","Simplex","Solar","United","Zephyr","Cosmo" }.Order().ToImmutableList();

    internal const string APPLY_REDIRECT_URL = $$"""&returnUrl={relativeUrl}""";
    internal const string APPLY_THEME_URL = $$"""Abp/Theme/Change?theme={name}""";
    internal const string APPLY_MENUSTYLE_URL= $$"""Abp/Style/Change?style={name}""";
    internal const string APPLY_LANGUAGE_URL = $$"""Abp/Languages/Switch?culture={cultureName}&uiCulture={uiCultureName}""";
}
