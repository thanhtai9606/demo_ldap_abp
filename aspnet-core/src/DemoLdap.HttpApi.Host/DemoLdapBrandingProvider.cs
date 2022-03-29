using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DemoLdap;

[Dependency(ReplaceServices = true)]
public class DemoLdapBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DemoLdap";
}
