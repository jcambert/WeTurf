using We.Turf.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace We.Turf.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TurfEntityFrameworkCoreModule),
    typeof(TurfApplicationContractsModule)
    )]
public class TurfDbMigratorModule : AbpModule
{

}
