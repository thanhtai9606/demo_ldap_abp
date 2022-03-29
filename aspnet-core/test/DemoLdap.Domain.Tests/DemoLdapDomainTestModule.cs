using DemoLdap.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DemoLdap;

[DependsOn(
    typeof(DemoLdapEntityFrameworkCoreTestModule)
    )]
public class DemoLdapDomainTestModule : AbpModule
{

}
