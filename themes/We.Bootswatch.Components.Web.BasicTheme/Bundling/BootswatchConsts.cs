using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class BootswatchConsts
{
    internal const string ThemeCookie = "theme";
    internal const string MenuStyleCookie = "menu_style";
    internal const string FluidCookie = "isfluid";
    internal const string DefaultTheme = "Darkly";

    //public static IReadOnlyList<string> SupportedThemes = new List<string>() { "Darkly", "Cerulean", "Litera", "Materia","Pulse","Simplex","Solar","United","Zephyr","Cosmo" }.Order().ToImmutableList();

    internal const string APPLY_REDIRECT_URL = $$"""&returnUrl={0}""";
    internal const string APPLY_THEME_URL = $$"""Abp/Theme/Change?returnUrl={0}&theme={1}""";
    internal const string APPLY_MENUSTYLE_URL= $$"""Abp/Style/Change?returnUrl={0}&style={1}""";
    internal const string APPLY_LANGUAGE_URL = $$"""Abp/Languages/Switch?returnUrl={0}&culture={1}&uiCulture={2}""";
    internal const string APPLY_MAINLAYOUT_FLUID_URL = $$"""Abp/MainLayout/Fluid?returnUrl={0}&isfluid={1}""";

}
