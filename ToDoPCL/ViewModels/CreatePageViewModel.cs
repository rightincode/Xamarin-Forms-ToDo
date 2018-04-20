using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo.Core.Interfaces;
using ToDo.Core.Models;
using ToDoPCL.Data;

namespace ToDoPCL.ViewModels
{
    public class CreatePageViewModel : INotifyPropertyChanged//, IValidatableObject
    {
        private IToDoItem mCurrentToDoItem;
        private string mTaskId;
        private string mTaskName;
        private string mPriority;
        private DateTime mDueDate;
        private TimeSpan mDueTime;

        private IDataStore<ToDoItem> mDataStore;

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

        public string Id
        {
            get
            {
                return mTaskId;
            }
            set
            {
                mTaskId = mCurrentToDoItem.Id = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
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

        public CreatePageViewModel(IToDoItem currentToDoItem, IDataStore<ToDoItem> dataStore)
        {
            mCurrentToDoItem = currentToDoItem;
            mDataStore = dataStore;
        }
                
        public async Task<bool> AddToDoItem()
        {
            mCurrentToDoItem.DueDate = this.SetDueDate(DueDate, DueTime.Hours, DueTime.Minutes,
                DueTime.Seconds);

            return await mDataStore.SaveItemAsync((ToDoItem)mCurrentToDoItem);
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
                mCurrentToDoItem = await mDataStore.GetItemAsync(toDoListItemId);
                Id = mCurrentToDoItem.Id;
                TaskName = mCurrentToDoItem.TaskName;
                Priority = mCurrentToDoItem.Priority;
                DueDate = mCurrentToDoItem.DueDate;

            } else
            {
                mCurrentToDoItem.SetToDoItemId();
            }

            DueTime = new TimeSpan(mCurrentToDoItem.DueDate.Hour, mCurrentToDoItem.DueDate.Minute,
                mCurrentToDoItem.DueDate.Second);

            return 0;
        }

        public async Task<bool> DeleteToDoItem()
        {
            return await mDataStore.DeleteItemAsync((ToDoItem)mCurrentToDoItem);
        }

        //not used at the moment
        public class AddToDoItemCommand : ICommand
        {
            event EventHandler ICommand.CanExecuteChanged
            {
                add
                {
                    throw new NotImplementedException();
                }

                remove
                {
                    throw new NotImplementedException();
                }
            }

            bool ICommand.CanExecute(object parameter)
            {
                //throw new NotImplementedException();
                return true;
            }

            void ICommand.Execute(object parameter)
            {
                //AddToDoItem();
            }
        }
    }
}
