using Volo.Abp.Modularity;

namespace Edary;

[DependsOn(
    typeof(EdaryDomainModule),
    typeof(EdaryTestBaseModule)
)]
public class EdaryDomainTestModule : AbpModule
{

}
