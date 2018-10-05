using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;

using ToDo.Data.Interfaces;
using ToDo.Core.Models;

namespace ToDo.UnitTests
{
    public class MockToDoItemDatabase : IToDoItemDatabase<ToDoItem>
    {
        private List<ToDoItem> localDataStore;

        public MobileServiceClient MobileService { get; set; }

        public MockToDoItemDatabase()
        {
            InitializeDB();
        }

        public Task<bool> DeleteItemAsync(ToDoItem item)
        {
            return Task.Run(() => {
                return localDataStore.Remove(item);
            });
        }

        public Task<ToDoItem> GetItemAsync(string id)
        {
            return Task.Run(() => {
                return localDataStore.Find(item => item.Id == id);
            });
        }

        public Task<List<ToDoItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return Task.Run(() =>
            {
                return localDataStore;
            });
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveItemAsync(ToDoItem item)
        {
            return Task.Run(() => {
                localDataStore.Add(item);
                return true;
            });
        }

        public Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        private void InitializeDB()
        {
            localDataStore = new List<ToDoItem>();
            ToDoItem testToDoItem = new ToDoItem {
                TaskName = "TestItem1",
                DueDate = new DateTime(2018, 04, 27, 4, 30, 0),
                Priority = "Low"
            };
            localDataStore.Add(testToDoItem);
        }
    }
}
