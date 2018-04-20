using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using ToDo.Core.Models;
using ToDoPCL.Interfaces;

namespace ToDoPCL.ViewModels
{
    public class ListTasksPageViewModel
    {
        private List<ToDoItem> toDoItems;
        private ToDoItem selectedItem;
        private IToDoItemDatabase<ToDoItem> mDataStore;

        public List<ToDoItem> ToDoItems
        {
            get
            {
                return toDoItems;
            }

            set
            {
                toDoItems = value;
            }
        }

        public ToDoItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
        }
        
        public ListTasksPageViewModel(IToDoItemDatabase<ToDoItem> dataStore)
        {
            mDataStore = dataStore;
        }

        public async Task<int> LoadItemsAsync(bool forceRefresh = false)
        {
            ToDoItems = await mDataStore.GetItemsAsync(forceRefresh);
            return ToDoItems.Count;
        }

        public void SaveSelectedItem(ItemTappedEventArgs e)
        {
            selectedItem = e.Item as ToDoItem;
        }
    }
}
