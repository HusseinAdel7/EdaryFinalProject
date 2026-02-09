using Volo.Abp.Modularity;

namespace Edary;

/* Inherit from this class for your domain layer tests. */
public abstract class EdaryDomainTestBase<TStartupModule> : EdaryTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
