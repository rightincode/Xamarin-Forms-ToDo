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
            
            BindingContext = VM;
            Clear();
        }

        public CreatePage(int toDoListItemId)
        {
            InitializeComponent();
            vm = new CreatePageViewModel();
            mTodoListItemId = (toDoListItemId > 0) ? toDoListItemId : 0;
            
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

        public async void OnSave(object o, EventArgs e) {
            await VM.AddToDoItem();
            Clear();      //causes problems if we don't wait for the database call above to complete - two way binding!!!!
            await Navigation.PopAsync();

        }

        public async void OnDelete(object o, EventArgs e)
        {
            await VM.DeleteToDoItem();
            Clear();
            await Navigation.PopAsync();
        }


        public void OnCancel(object o, EventArgs e) {
            Clear();
            Navigation.PopAsync();
        }
     
    }
}
