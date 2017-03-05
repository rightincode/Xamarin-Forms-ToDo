using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoPCL.Models
{
    public class ToDoItem
    {
        public string TaskName { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDeleated { get; set; }

        public ToDoItem( string taskName, string priority, DateTime dueDate, bool isDeleated)
        {
            TaskName = taskName;
            Priority = priority;
            DueDate = dueDate;
            IsDeleated = isDeleated;
        }
    }
}
