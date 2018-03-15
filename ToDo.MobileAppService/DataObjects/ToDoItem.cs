using System;
using Microsoft.Azure.Mobile.Server;

namespace ToDo.MobileAppService.DataObjects
{
    public class ToDoItem : EntityData
    {
        public string TaskName { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}