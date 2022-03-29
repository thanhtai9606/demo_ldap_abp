using DemoLdap.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace DemoLdap.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DemoLdapEntityFrameworkCoreModule),
    typeof(DemoLdapApplicationContractsModule)
    )]
public class DemoLdapDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
