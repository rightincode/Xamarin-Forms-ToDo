using System;
using System.ComponentModel;
using System.Threading.Tasks;

using ToDoPCL.Models;

namespace ToDoPCL.ViewModels
{
    public class CreatePageViewModel : INotifyPropertyChanged
    {
        private ToDoItem mCurrentToDoItem;
        private string mTaskName;
        private string mPriority;
        private DateTime mDueDate;
        private TimeSpan mDueTime;

        public event PropertyChangedEventHandler PropertyChanged;

        //public ToDoItem CurrentToDoItem
        //{
        //    get
        //    {
        //        return mCurrentToDoItem;
        //    }
        //    set
        //    {
        //        mCurrentToDoItem = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("CurrentToDoItem.TaskName"));
        //            PropertyChanged(this, new PropertyChangedEventArgs("CurrentToDoItem.Priority"));
        //            PropertyChanged(this, new PropertyChangedEventArgs("CurrentToDoItem.DueDate"));
        //        }

        //    }
        //}

        public string TaskName
        {
            get
            {
                return mTaskName;
            }
            set
            {
                mTaskName = mCurrentToDoItem.TaskName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TaskName"));
                }
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
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
                }
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
                mDueDate = mCurrentToDoItem.DueDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DueDate"));
                }
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
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DueTime"));
                }
            }
        }

        public CreatePageViewModel()
        {
            mCurrentToDoItem = new ToDoItem();
        }
                
        public Task<int> AddToDoItem()
        {

            mCurrentToDoItem.DueDate = this.SetDueDate(DueDate, DueTime.Hours, DueTime.Minutes,
                DueTime.Seconds);

            return ToDoPCL.Database.SaveItemAsync(mCurrentToDoItem);
        }
        
        private DateTime SetDueDate(DateTime date, int hour, int minute, int second)
        {
            DateTime retVal = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);

            return retVal;
        }

        public async Task<int> LoadToDoListItem(string toDoListItemId)
        {
            if (!string.IsNullOrEmpty(toDoListItemId))
            {
                mCurrentToDoItem = await ToDoPCL.Database.GetItemAsync(toDoListItemId);
                TaskName = mCurrentToDoItem.TaskName;
                Priority = mCurrentToDoItem.Priority;
                DueDate = mCurrentToDoItem.DueDate;

            }

            DueTime = new TimeSpan(mCurrentToDoItem.DueDate.Hour, mCurrentToDoItem.DueDate.Minute,
                mCurrentToDoItem.DueDate.Second);

            return 0;
        }

        public Task<int> DeleteToDoItem()
        {
            return ToDoPCL.Database.DeleteItemAsync(mCurrentToDoItem);
        }
    }
}
