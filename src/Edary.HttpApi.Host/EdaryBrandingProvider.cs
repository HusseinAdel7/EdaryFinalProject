using Microsoft.Extensions.Localization;
using Edary.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Edary;

[Dependency(ReplaceServices = true)]
public class EdaryBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EdaryResource> _localizer;

    public EdaryBrandingProvider(IStringLocalizer<EdaryResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
