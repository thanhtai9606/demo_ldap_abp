using Volo.Abp.Modularity;

namespace DemoLdap;

[DependsOn(
    typeof(DemoLdapApplicationModule),
    typeof(DemoLdapDomainTestModule)
    )]
public class DemoLdapApplicationTestModule : AbpModule
{

}
