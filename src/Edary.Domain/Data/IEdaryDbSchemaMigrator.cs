using System.Threading.Tasks;

namespace Edary.Data;

public interface IEdaryDbSchemaMigrator
{
    Task MigrateAsync();
}
