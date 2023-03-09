using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.Modularity;
using We.Bootswatch.Components.Web.BasicTheme.Services;

namespace We.Bootswatch.Components.Web.BasicTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule)
    )]
public class WeAspNetCoreComponentsWebBasicThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        var configuration = context.Services.GetConfiguration();
        
        
        context.Services.AddScoped<IMenuStyleProvider, MenuStyleProvider>();
        context.Services.AddScoped<IThemeProvider, ThemeProvider>();
        context.Services.AddScoped<ILangProvider,LangProvider>();
        //context.Services.AddTransient<IWeMenuStyleManager, WeMenuStyleManager>();
        //context.Services.AddTransient<WeMenuStyleOptions>();
    }
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<LayoutOptions>(options =>
        {
           
        });
    }
        
}
