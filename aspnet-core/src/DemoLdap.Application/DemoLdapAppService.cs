using System;
using System.Collections.Generic;
using System.Text;
using DemoLdap.Localization;
using Volo.Abp.Application.Services;

namespace DemoLdap;

/* Inherit your application services from this class.
 */
public abstract class DemoLdapAppService : ApplicationService
{
    protected DemoLdapAppService()
    {
        LocalizationResource = typeof(DemoLdapResource);
    }
}
