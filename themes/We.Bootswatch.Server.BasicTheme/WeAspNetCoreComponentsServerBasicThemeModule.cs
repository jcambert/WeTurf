﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Components.Server.Theming.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme;

[DependsOn(
    typeof(WeAspNetCoreComponentsWebBasicThemeModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(AbpLocalizationModule)
    )]
public class WeAspNetCoreComponentsServerBasicThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new BasicThemeToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorBasicThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorBasicThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorBasicThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorBasicThemeScriptContributor));
                });
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            //Define a new localization resource (TestResource)
            options.Resources
                .Add<ThemeResource>()
                .AddVirtualJson("/Localization/Resources/Theme");
        });

        //context.Services.AddScoped<IThemeProvider,ThemeProvider>();
        
    }
}
