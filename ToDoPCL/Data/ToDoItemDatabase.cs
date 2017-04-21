using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;

using ToDoPCL.Models;

namespace ToDoPCL.Data
{
    public class ToDoItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ToDoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDoItem>().Wait();
        }

        public Task<List<ToDoItem>> GetItemsAsync()
        {
            return database.Table<ToDoItem>().ToListAsync();
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

    }
}
