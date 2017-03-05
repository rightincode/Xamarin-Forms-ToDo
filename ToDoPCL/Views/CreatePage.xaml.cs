using System;
using Xamarin.Forms;

using ToDoPCL.ViewModels;

namespace ToDoPCL
{
	public partial class CreatePage : ContentPage
	{
        private CreatePageViewModel vm;

		public CreatePage ()
		{
            vm = new CreatePageViewModel();
			InitializeComponent ();            
		}
       
        private void Clear()
        {
            ToDoEntry.Text = Priority.Text = String.Empty;
            Date.Date = DateTime.Now;
            Time.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public void OnSave(object o, EventArgs e) {
            vm.AddToDoItem(ToDoEntry.Text, Priority.Text, Date.Date, Time.Time);
            Clear();
        }

        public void OnCancel(object o, EventArgs e) { }

        public void OnReview(object o, EventArgs e) {
            Clear();
            Navigation.PushAsync(new ListTasksPage(vm));
        }
    }
}
