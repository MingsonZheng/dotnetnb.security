using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core.Store
{
    public class DefaultPermissionStore : IPermissionStore
    {
        private List<Permission> _list;

        public DefaultPermissionStore()
        {
            _list = new List<Permission>();
        }

        public async Task CreateAsync(Permission permission)
        {
            _list.Add(permission);
        }

        public async Task<Permission> GetByKeyAsync(string key)
        {
            return _list.SingleOrDefault(r => r.Key == key);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return _list;
        }
    }
}
