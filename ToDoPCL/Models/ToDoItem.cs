using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace ToDoPCL.Models
{
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }

        public ToDoItem()
        {
        }

        public ToDoItem(string taskName, string priority, DateTime dueDate)
        {
            TaskName = taskName;
            Priority = priority;
            DueDate = dueDate;
        }
    }
}
