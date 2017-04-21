using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoPCL.Data
{
    public interface IDataStore<T>
    {

        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> PullLatestAsync();

        Task InitializeAsync();
    }
}
