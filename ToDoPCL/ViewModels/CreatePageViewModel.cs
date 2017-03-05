using System;
using System.Collections.Generic;

using ToDoPCL.Models;

namespace ToDoPCL.ViewModels
{
    public class CreatePageViewModel
    {
        public List<ToDoItem> ToDoItems
        {
            get
            {
                return toDoItems;
            }
        }

        private List<ToDoItem> toDoItems;

        public CreatePageViewModel()
        {
            if (toDoItems == null)
            {
                toDoItems = new List<ToDoItem>();
            }
        }
        
        public void AddToDoItem(string toDoText, string toDoPriority, DateTime date, TimeSpan time)
        {
            var mNewToDoItem = new ToDoItem(toDoText, toDoPriority, 
                this.SetDueDate(date, time.Hours, time.Minutes, time.Seconds), false);

            this.toDoItems.Add(mNewToDoItem);
        }
        
        private DateTime SetDueDate(DateTime date, int hour, int minute, int second)
        {
            DateTime retVal = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);

            return retVal;
        }

    }
}
