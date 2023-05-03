using Volo.Abp.Modularity;

namespace We.Turf;

[DependsOn(typeof(TurfApplicationModule), typeof(TurfDomainTestModule))]
public class TurfApplicationTestModule : AbpModule { }
