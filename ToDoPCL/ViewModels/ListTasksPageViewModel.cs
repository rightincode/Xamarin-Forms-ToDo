using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using ToDoPCL.Models;

namespace ToDoPCL.ViewModels
{
    public class ListTasksPageViewModel
    {
        public IEnumerable<ToDoItem> ToDoItems
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

        private IEnumerable<ToDoItem> toDoItems;
        private ToDoItem selectedItem;

        public ListTasksPageViewModel()
        {
        }

        public async Task<int> LoadItemsAsync(bool forceRefresh = false)
        {
            ToDoItems = await ToDoPCL.Database.GetItemsAsync(forceRefresh);
            return 0;
        }

        public void SaveSelectedItem(ItemTappedEventArgs e)
        {
            selectedItem = e.Item as ToDoItem;
        }
    }
}
