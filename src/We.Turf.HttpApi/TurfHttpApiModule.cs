using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using We.Turf.Localization;

namespace We.Turf;

[DependsOn(
    typeof(TurfApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class TurfHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
        ConfigureAutoApiControllers();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TurfResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            // options.ConventionalControllers.Create(typeof(TurfApplicationModule).Assembly);
            options.ConventionalControllers.Create(typeof(TurfHttpApiModule).Assembly);
            //options.ConventionalControllers.Create(typeof(WeAspNetCoreComponentsServerBasicThemeModule).Assembly);

        });
    }


}
