using System.Collections.Generic;

using Xamarin.Forms;
using ToDoPCL.Models;

namespace ToDoPCL.ViewModels
{
    public class ListTasksPageViewModel
    {
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

        private List<ToDoItem> toDoItems;
        private ToDoItem selectedItem;

        public ListTasksPageViewModel()
        {
            if (toDoItems == null)
            {
                toDoItems = new List<ToDoItem>();
            }
        }

        public void SaveSelectedItem(ItemTappedEventArgs e)
        {
            selectedItem = e.Item as ToDoItem;
        }
    }
}
