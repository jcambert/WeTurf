using We.Turf.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace We.Turf;

[DependsOn(typeof(TurfEntityFrameworkCoreTestModule))]
public class TurfDomainTestModule : AbpModule { }
