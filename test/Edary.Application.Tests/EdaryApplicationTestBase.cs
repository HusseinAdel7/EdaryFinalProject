using Volo.Abp.Modularity;

namespace Edary;

public abstract class EdaryApplicationTestBase<TStartupModule> : EdaryTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
