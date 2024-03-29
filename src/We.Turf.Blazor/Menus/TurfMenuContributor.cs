using System.Threading.Tasks;
using We.Turf.Localization;
using We.Turf.MultiTenancy;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace We.Turf.Blazor.Menus;

public class TurfMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<TurfResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TurfMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                TurfMenus.Scrap,
                l["Menu:Scrap"],
                "/scrap",
                icon: "fas fa-home",
                order: 1
            )
        );
        context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                TurfMenus.Parameters,
                l["Menu:Parameters"],
                "/parameters",
                icon: "fas fa-gear",
                order: 1
            )
        );
        context.Menu.Items.Insert(
            3,
            new ApplicationMenuItem(
                TurfMenus.Swagger,
                l["Menu:Swagger"],
                "/swagger",
                icon: "fas fa-fire",
                order: 1
            )
        );

#pragma warning disable CS1634,CS0162,IDE0035
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
#pragma warning restore CS1634,CS0162,IDE0035

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
