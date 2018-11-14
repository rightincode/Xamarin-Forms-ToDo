using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo.Core.Interfaces;
using ToDo.Core.Models;
using ToDo.Data.Interfaces;

namespace ToDo.ViewModels
{
    public class CreatePageViewModel : BaseViewModel
    {
        private IToDoItem mCurrentToDoItem;
        private string mTaskId;
        private string mTaskName;
        private string mPriority;
        private DateTime mDueDate;
        private TimeSpan mDueTime;

        private IToDoItemDatabase<ToDoItem> mDataStore;

        public string Id
        {
            get
            {
                return mTaskId;
            }
            set
            {
                mTaskId = mCurrentToDoItem.Id = value;
                OnPropertyChanged("Id");                
            }
        }

        public string TaskName
        {
            get
            {
                return mTaskName;
            }
            set
            {
                mTaskName = mCurrentToDoItem.TaskName = value;
                OnPropertyChanged("TaskName");
            }
        }

        public string Priority
        {
            get
            {
                return mPriority;
            }

            set
            {
                mPriority = mCurrentToDoItem.Priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public DateTime DueDate
        {
            get
            {
                return mDueDate;
            }
            set
            {
                mDueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        public TimeSpan DueTime
        {
            get
            {
                return mDueTime;
            }
            set
            {
                mDueTime = value;
                OnPropertyChanged("DueTime");
            }
        }

        public CreatePageViewModel(IToDoItem currentToDoItem, IToDoItemDatabase<ToDoItem> dataStore)
            : base()
        {            
            mCurrentToDoItem = currentToDoItem;
            DueDate = currentToDoItem.DueDate;
            DueTime = GetDueTimeFromToDoItem(currentToDoItem);

            mDataStore = dataStore;
        }
                
        public async Task<bool> AddToDoItem()
        {
            mCurrentToDoItem.DueDate = this.SetDueDate(DueDate, DueTime.Hours, DueTime.Minutes,
                DueTime.Seconds);

            return await mDataStore.SaveItemAsync((ToDoItem)mCurrentToDoItem);
        }
                
        public async Task LoadToDoListItem(string toDoListItemId)
        {
            if (!string.IsNullOrEmpty(toDoListItemId))
            {
                mCurrentToDoItem = await mDataStore.GetItemAsync(toDoListItemId);
                Id = mCurrentToDoItem.Id;
                TaskName = mCurrentToDoItem.TaskName;
                Priority = mCurrentToDoItem.Priority;
                DueDate = mCurrentToDoItem.DueDate;
                DueTime = GetDueTimeFromToDoItem(mCurrentToDoItem);
            }
        }

        public async Task<bool> DeleteToDoItem()
        {
            return await mDataStore.DeleteItemAsync((ToDoItem)mCurrentToDoItem);
        }

        private DateTime SetDueDate(DateTime date, int hour, int minute, int second)
        {
            DateTime retVal = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);

            return retVal;
        }

        private TimeSpan GetDueTimeFromToDoItem(IToDoItem currentToDoItem)
        {
            TimeSpan mDueTime;

            try
            {
                mDueTime = new TimeSpan(mCurrentToDoItem.DueDate.Hour, mCurrentToDoItem.DueDate.Minute,
                                mCurrentToDoItem.DueDate.Second);
            }
            catch (ArgumentOutOfRangeException)
            {
                mDueTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }

            return mDueTime;
        }
    }
}
