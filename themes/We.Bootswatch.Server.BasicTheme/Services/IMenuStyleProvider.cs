using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme.Services;
/*
public interface IWeMenuStyle : INameable, IEquatable<IWeMenuStyle>, IEqualityComparer<IWeMenuStyle>
{
    WeMenuStyle Style { get; }
}

internal abstract class MenuStyle : IWeMenuStyle
{
    internal static readonly MenuStyle Default = new StyleDefault();
    internal static readonly MenuStyle Left = new StyleLeft();

    internal static readonly List<IWeMenuStyle> Styles = new List<IWeMenuStyle>();
    protected MenuStyle(string name,WeMenuStyle style) {
        Name = name;
        Style = style;
        Styles.Add(this);
    }

    public WeMenuStyle Style { get; }
    public string Name { get; }

    public bool Equals(IWeMenuStyle other)
    {
        if (other == null) return false;
        return GetType() == other.GetType() && Name == other.Name;
    }

    public bool Equals(IWeMenuStyle x, IWeMenuStyle y)
     => x.Name == y.Name;

    public int GetHashCode([DisallowNull] IWeMenuStyle obj)
    => obj.Name.GetHashCode();

    #region internal classes
    private sealed class StyleDefault : MenuStyle
    {
        public StyleDefault() : base("Top Side", WeMenuStyle.TopSide)
        {
        }
    }

    private sealed class StyleLeft : MenuStyle
    {
        public StyleLeft() : base("Left Side", WeMenuStyle.LeftSide)
        {
        }
    }
    #endregion
}

public interface IMenuStyleProvider:ISelectorProvider<IWeMenuStyle>
{
}

public class MenuStyleProvider : SelectorProvider<IWeMenuStyle>, IMenuStyleProvider
{

    public MenuStyleProvider(IHttpContextAccessor context, NavigationManager navigationManager) : base(context, navigationManager)
    {
    }

    protected override IWeMenuStyle Default => MenuStyle.Default;
    protected override List<IWeMenuStyle> Values => MenuStyle.Styles;
    protected override string CookieName =>BootswatchConsts.MenuStyleCookie;
}
*/