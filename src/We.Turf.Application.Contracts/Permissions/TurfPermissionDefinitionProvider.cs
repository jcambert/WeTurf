using We.Turf.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace We.Turf.Permissions;

public class TurfPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TurfPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TurfPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TurfResource>(name);
    }
}
