using Edary.Localization;
using Volo.Abp.Application.Services;

namespace Edary;

/* Inherit your application services from this class.
 */
public abstract class EdaryAppService : ApplicationService
{
    protected EdaryAppService()
    {
        LocalizationResource = typeof(EdaryResource);
    }
}
