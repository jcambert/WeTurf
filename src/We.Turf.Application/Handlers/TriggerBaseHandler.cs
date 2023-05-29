using We.AbpExtensions;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public abstract class TriggerBaseHandler<TQuery, TResponse> : AbpHandler.With<TQuery, TResponse>
    where TQuery : WeM.IQuery<TResponse>
    where TResponse : WeM.Response
{
    protected IRepository<ScrapTrigger, Guid> Repository =>
        GetRequiredService<IRepository<ScrapTrigger, Guid>>();

    protected TriggerBaseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected ScrapTrigger Map(ScrapTriggerDto triggerDto) =>
        ObjectMapper.Map<ScrapTriggerDto, ScrapTrigger>(triggerDto);

    protected ScrapTrigger Map(ScrapTriggerDto triggerDto, ScrapTrigger trigger) =>
        ObjectMapper.Map(triggerDto, trigger);

    protected ScrapTriggerDto ReverseMap(ScrapTrigger trigger) =>
        ObjectMapper.Map<ScrapTrigger, ScrapTriggerDto>(trigger);
}
