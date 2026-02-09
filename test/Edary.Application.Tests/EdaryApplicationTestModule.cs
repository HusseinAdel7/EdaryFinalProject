using Volo.Abp.Modularity;

namespace Edary;

[DependsOn(
    typeof(EdaryApplicationModule),
    typeof(EdaryDomainTestModule)
)]
public class EdaryApplicationTestModule : AbpModule
{

}
