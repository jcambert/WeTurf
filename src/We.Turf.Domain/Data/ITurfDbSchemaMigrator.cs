using System.Threading.Tasks;

namespace We.Turf.Data;

public interface ITurfDbSchemaMigrator
{
    Task MigrateAsync();
}
