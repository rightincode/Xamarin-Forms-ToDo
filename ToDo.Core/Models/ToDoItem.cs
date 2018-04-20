using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using ToDo.Core.Interfaces;

namespace ToDo.Core.Models
{
    public class ToDoItem : IToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "taskName")]
        public string TaskName { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; set; }
        [JsonProperty(PropertyName = "dueDate")]
        public DateTime DueDate { get; set; }

        //azure specific attributes
        [JsonProperty(PropertyName = "createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [UpdatedAt]
        public DateTimeOffset UpdatedAt { get; set; }

        [Version]
        public string AzureVersion { get; set; }
        
        public ToDoItem()
        {            
        }
        
        public void SetToDoItemId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
