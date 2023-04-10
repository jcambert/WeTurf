using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using We.Processes;

namespace We.Turf;

[DependsOn(
    typeof(TurfDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(TurfApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class TurfApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TurfApplicationModule>();
        });
        context.Services.UsePythonExecutor(opt =>
        {
            opt.UseAnaconda = true;
            opt.UseReactiveOutput=true;
            
        });
    }
}
