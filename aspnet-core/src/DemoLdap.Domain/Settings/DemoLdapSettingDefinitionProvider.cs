using Volo.Abp.Settings;

namespace DemoLdap.Settings;

public class DemoLdapSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(DemoLdapSettings.MySetting1));
    }
}
