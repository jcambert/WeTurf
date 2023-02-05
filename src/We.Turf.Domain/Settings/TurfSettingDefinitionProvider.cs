using Volo.Abp.Settings;

namespace We.Turf.Settings;

public class TurfSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TurfSettings.MySetting1));
    }
}
