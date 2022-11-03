using unitofwork_core.Entities;
namespace unitofwork_core.Core.IRepository
{
    public interface IConfigRepostiory : IGenericRepository<ConfigApp>
    {
        string GetValueConfig(string configName);
    }
}
