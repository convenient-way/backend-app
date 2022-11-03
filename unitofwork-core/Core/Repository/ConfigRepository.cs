using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class ConfigRepository : GenericRepository<ConfigApp>, IConfigRepostiory
    {
        public ConfigRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public string GetValueConfig(string configName)
        {
            ConfigApp? configApp = _dbSet.SingleOrDefault(con => con.Name.Equals(configName));
            if (configApp != null) {
                return configApp.Note;
            }
            return "";
        }
    }
}
