using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using We.Bootswatch.Server.BasicTheme.Themes.Basic;

namespace We.Bootswatch.Server.BasicTheme;

public class BasicThemeToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            //context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));

            context.Toolbar.Items.Add(new ToolbarItem(typeof(LangSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(ThemeSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(MenuStyleSelector)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(FluidSwitch)));
        }

        return Task.CompletedTask;
    }
}
