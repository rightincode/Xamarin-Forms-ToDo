using Xamarin.Forms;

using ToDoPCL.ViewModels;

namespace ToDoPCL
{
	public partial class ListTasksPage : ContentPage
	{
        public ListTasksPageViewModel VM
        {
            get
            {
                return vm;
            }
        }

        private ListTasksPageViewModel vm;

        public ListTasksPage()
		{
            vm = new ListTasksPageViewModel();
            BindingContext = this;
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VM.LoadItemsAsync();
            this.ToDoList.ItemsSource = null;
            this.ToDoList.ItemsSource = VM.ToDoItems;
        }

        public void OnSelected(object o, ItemTappedEventArgs e)
        {
            VM.SaveSelectedItem(e);
            DisplayAlert("Chosen!", VM.SelectedItem.TaskName + " was selected!", "Ok");
        }
	}
}
