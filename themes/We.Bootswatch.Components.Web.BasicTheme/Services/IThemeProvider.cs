using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme;


public interface INameable
{
    string Name { get; }

}

public interface ITheme : INameable, IEquatable<ITheme>, IEqualityComparer<ITheme>
{
    string Menu { get; }
}

internal abstract class Theme : ITheme
{

    internal static readonly List<ITheme> Themes = new ();
    internal static readonly ITheme Default = new ThemeDefault();
    internal static readonly ITheme Darkly = new ThemeDarkly();
    internal static readonly ITheme Cerulean = new ThemeCerulean();
    internal static readonly ITheme Litera = new ThemeLitera();
    protected Theme(string name, string menu)
    {
        this.Name = name;
        this.Menu = menu;
        Themes.Add(this);
    }
    public Theme(string name) : this(name, $"Menu:Themes:{name}")
    {

    }
    public string Name { get; protected init; }
    public string Menu { get; protected init; }

    public bool Equals(ITheme? other)
    {
        if (other == null) return false;
        return GetType() == other.GetType() && Name == other.Name;
    }

    public bool Equals(ITheme? x, ITheme? y)
        => x?.Name == y?.Name;

    public int GetHashCode([DisallowNull] ITheme obj)
        => obj.Name.GetHashCode();

    #region internal Classes
    private sealed class ThemeDefault : Theme
    {
        public ThemeDefault() : base(BootswatchConsts.DefaultTheme)
        {

        }
    }
    private sealed class ThemeDarkly : Theme
    {
        public ThemeDarkly() : base("Darkly")
        {
        }
    }
    private sealed class ThemeCerulean : Theme
    {
        public ThemeCerulean() : base("Cerulean")
        {

        }
    }
    private sealed class ThemeLitera : Theme
    {
        public ThemeLitera() : base("Litera")
        {

        }
    }
    #endregion

}
public interface IThemeProvider:ISelectorProvider<ITheme>
{


}
public class ThemeProvider : SelectorProvider<ITheme>, IThemeProvider
{

    
    public ThemeProvider(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context, NavigationManager navigationManager) : base(serviceProvider, context,navigationManager)
    {
    }

    protected override ITheme Default => Values.FirstOrDefault(t => t.Name ==  Options.Value.Theme) ?? Theme.Default;
    protected override List<ITheme> Values => Theme.Themes;
    protected override string CookieName => BootswatchConsts.ThemeCookie;
}

