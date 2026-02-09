using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Edary.Data;

/* This is used if database provider does't define
 * IEdaryDbSchemaMigrator implementation.
 */
public class NullEdaryDbSchemaMigrator : IEdaryDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
