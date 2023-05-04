using Microsoft.AspNetCore.Components;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class TopNavbar
{
    [Parameter]
    public bool IsCollapseShown { get; set; }

    private void ToggleCollapse()
    {
        IsCollapseShown = !IsCollapseShown;
    }
}
