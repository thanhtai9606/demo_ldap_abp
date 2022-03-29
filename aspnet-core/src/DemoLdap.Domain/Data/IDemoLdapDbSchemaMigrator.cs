using System.Threading.Tasks;

namespace DemoLdap.Data;

public interface IDemoLdapDbSchemaMigrator
{
    Task MigrateAsync();
}
