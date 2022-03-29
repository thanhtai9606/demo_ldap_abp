using DemoLdap.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DemoLdap.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DemoLdapController : AbpControllerBase
{
    protected DemoLdapController()
    {
        LocalizationResource = typeof(DemoLdapResource);
    }
}
