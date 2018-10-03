using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Interfaces
{
    public interface IToDoItemDatabase<T>
    {
        MobileServiceClient MobileService { set; get; }

        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemAsync(string id);
        Task<bool> SaveItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);

        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();

        Task InitializeAsync();
    }
}
