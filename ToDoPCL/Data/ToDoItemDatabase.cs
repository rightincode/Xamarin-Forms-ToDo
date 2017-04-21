using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SQLite;
using Xamarin.Forms;

using ToDoPCL.Models;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace ToDoPCL.Data
{
    public class ToDoItemDatabase : IDataStore<ToDoItem>
    {
        readonly SQLiteAsyncConnection database;
        private string _dbPath;
        private readonly string azureMobileAppUrl = "https://xformstodo.azurewebsites.net";

        public bool isInitialized;
        IMobileServiceSyncTable<ToDoItem> itemsTable;

        public MobileServiceClient MobileService { get; set; }

        public ToDoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDoItem>().Wait();

            _dbPath = dbPath;
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
            {
                return;
            }

            //AuthenticationHandler handler = null;

            //if (UseAuthentication)
            //    handler = new AuthenticationHandler();

            MobileService = new MobileServiceClient(azureMobileAppUrl)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings
                {
                    CamelCasePropertyNames = true
                }
            };

            //if (UseAuthentication && !string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            //{
            //    MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
            //    MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            //}

            var store = new MobileServiceSQLiteStore(_dbPath);
            store.DefineTable<ToDoItem>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            itemsTable = MobileService.GetSyncTable<ToDoItem>();

            isInitialized = true;
        }

        public async Task<List<ToDoItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            if (forceRefresh)
                await PullLatestAsync();

            return await itemsTable.ToListAsync();
        }

        public Task<ToDoItem> GetItemAsync(string id)
        {
            return database.Table<ToDoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ToDoItem item)
        {
            if (GetItemAsync(item.Id) != null)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ToDoItem item)
        {
            return database.DeleteAsync(item);
        }

        public async Task<bool> PullLatestAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to pull items, we are offline");
                return false;
            }
            try
            {
                await itemsTable.PullAsync($"all{typeof(ToDoItem).Name}", itemsTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull items, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            return true;
        }
    }
}
