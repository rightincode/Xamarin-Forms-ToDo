using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoPCL.Data
{
    public interface IDataStore<T>
    {
        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemAsync(string id);
        Task<bool> SaveItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);

        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();

        Task InitializeAsync();
    }
}
