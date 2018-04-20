using System;
using Xamarin.Forms;

using ToDoPCL.ViewModels;
using ToDo.Core.Models;

namespace ToDoPCL
{
	public partial class CreatePage : ContentPage
	{
        private CreatePageViewModel vm;
        private string mTodoListItemId;

        public CreatePageViewModel VM
        {
            get
            {
                return vm;
            }
        }

		public CreatePage()
		{           
            InitializeComponent();
            WireUpEventHandlers();
            vm = new CreatePageViewModel(new ToDoItem(), ToDoPCL.Database);
            mTodoListItemId = string.Empty;
            BindingContext = VM;
            Clear();
        }

        public CreatePage(string toDoListItemId)
        {
            InitializeComponent();
            WireUpEventHandlers();
            vm = new CreatePageViewModel(new ToDoItem(), ToDoPCL.Database);
            mTodoListItemId = toDoListItemId;
            BindingContext = VM;
            Clear();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VM.LoadToDoListItem(mTodoListItemId);
        }

        private void Clear()
        {
            TaskName.Text = Priority.Text = String.Empty;
            DueDate.Date = DateTime.Now;
            DueTime.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        private async void OnSave(object o, EventArgs e) {
            await VM.AddToDoItem();
            Clear();      //causes problems if we don't wait for the database call above to complete - two way binding!!!!
            await Navigation.PopAsync();

        }

        private async void OnDelete(object o, EventArgs e)
        {
            await VM.DeleteToDoItem();
            Clear();
            await Navigation.PopAsync();
        }
        
        private void OnCancel(object o, EventArgs e) {
            Clear();
            Navigation.PopAsync();
        }

        private void WireUpEventHandlers()
        {
            saveToDoItemBtn.Clicked += OnSave;
            deleteToDoItemBtn.Clicked += OnDelete;
            cancelBtn.Clicked += OnCancel;
        }
     
    }
}
