using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace We.Turf.Data;

/* This is used if database provider does't define
 * ITurfDbSchemaMigrator implementation.
 */
public class NullTurfDbSchemaMigrator : ITurfDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
