using System;
using Xamarin.Forms;

using ToDoPCL.ViewModels;

namespace ToDoPCL
{
	public partial class CreatePage : ContentPage
	{
        private CreatePageViewModel vm;
        private int mTodoListItemId;

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
            vm = new CreatePageViewModel();
            mTodoListItemId = 0;
            Clear();
            BindingContext = VM;
        }

        public CreatePage(int toDoListItemId)
        {
            InitializeComponent();
            vm = new CreatePageViewModel();
            mTodoListItemId = (toDoListItemId > 0) ? toDoListItemId : 0;
            Clear();
            BindingContext = VM;            
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

        public async void OnSave(object o, EventArgs e) {
            await VM.AddToDoItem();
            Clear();      //causes problems if we don't wait for the database call to complete - two way binding!!!!
            await Navigation.PopAsync();
        }

        public void OnCancel(object o, EventArgs e) { }

        public async void OnReview(object o, EventArgs e) {
            Clear();
            await Navigation.PopAsync();
        }
       
    }
}
