using System;

namespace ToDo.Core.Interfaces
{
    public interface IToDoItem
    {
        string Id { get; set; }

        string TaskName { get; set; }

        string Priority { get; set; }

        DateTime DueDate { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset UpdatedAt { get; set; }

        string AzureVersion { get; set; }

        void SetToDoItemId();

    }
}
