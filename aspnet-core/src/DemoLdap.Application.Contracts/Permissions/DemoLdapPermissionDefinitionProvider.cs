using DemoLdap.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DemoLdap.Permissions;

public class DemoLdapPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DemoLdapPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(DemoLdapPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DemoLdapResource>(name);
    }
}
