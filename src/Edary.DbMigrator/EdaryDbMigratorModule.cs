using Edary.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Edary.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EdaryEntityFrameworkCoreModule),
    typeof(EdaryApplicationContractsModule)
)]
public class EdaryDbMigratorModule : AbpModule
{
}
