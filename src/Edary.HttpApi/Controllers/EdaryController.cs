using Edary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Edary.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EdaryController : AbpControllerBase
{
    protected EdaryController()
    {
        LocalizationResource = typeof(EdaryResource);
    }
}
