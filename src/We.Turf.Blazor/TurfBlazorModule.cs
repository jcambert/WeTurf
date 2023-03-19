using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using System;
using System.IO;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme;
using We.Bootswatch.Server.BasicTheme;
using We.Turf.Blazor.Bundling;
using We.Turf.Blazor.Menus;
using We.Turf.EntityFrameworkCore;
using We.Turf.Localization;
using We.Turf.MultiTenancy;

namespace We.Turf.Blazor;

[DependsOn(
    typeof(TurfApplicationModule),
    typeof(TurfEntityFrameworkCoreModule),
    typeof(TurfHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpIdentityBlazorServerModule),
    typeof(AbpTenantManagementBlazorServerModule),
    typeof(AbpSettingManagementBlazorServerModule),
    typeof(WeAspNetCoreComponentsServerBasicThemeModule), 
    typeof(WeAspNetCoreComponentsWebBasicThemeModule)
   )]
public class TurfBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(TurfResource),
                typeof(TurfDomainModule).Assembly,
                typeof(TurfDomainSharedModule).Assembly,
                typeof(TurfApplicationModule).Assembly,
                typeof(TurfApplicationContractsModule).Assembly,
                typeof(TurfBlazorModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Turf");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureJsonConverters();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
        ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureMediator(context);
    }

    private void ConfigureMediator(ServiceConfigurationContext context)
    {
        context.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(AbpExtensionsModule).Assembly,
                typeof(TurfApplicationModule).Assembly,
                typeof(WeAspNetCoreComponentsWebBasicThemeModule).Assembly
            );
        });
        
    }

    private void ConfigureJsonConverters()
    {
        Configure<JsonSerializerOptions>(options =>
        {
            options.Converters.Add(new We.Turf.Converters.TimeOnlyConverter());
            options.Converters.Add(new We.Turf.Converters.DateOnlyConverter());
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
           

            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorBasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/We.Turf.Blazor.styles.css");
                    bundle.AddFiles("/global-styles.css");
                    bundle.AddContributors(typeof(BootswatchStyleContributor));
                }
            );

           

        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<TurfDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}We.Turf.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<TurfDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}We.Turf.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<TurfApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}We.Turf.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<TurfApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}We.Turf.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<TurfBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        
        services.AddAbpSwaggerGen(
            options =>
            {
                
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Turf API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
                
                options.MapType<DateOnly>(()=> new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                });
            }
        );
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TurfMenuContributor());
        });
       /* Configure<WeMenuStyleOptions>(options =>
        {
            options.DefaultStyle = WeMenuStyle.LeftSide;
        });*/
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(TurfBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
           // options.ConventionalControllers.Create(typeof(TurfApplicationModule).Assembly);
            //options.ConventionalControllers.Create(typeof(TurfHttpApiModule).Assembly);
           //options.ConventionalControllers.Create(typeof(WeAspNetCoreComponentsServerBasicThemeModule).Assembly);
           
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TurfBlazorModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger(options =>
        {
            
        });
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Turf API");
            
        });
        app.UseConfiguredEndpoints();
    }
}
