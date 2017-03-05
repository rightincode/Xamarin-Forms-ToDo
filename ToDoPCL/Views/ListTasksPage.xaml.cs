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

        public ListTasksPage (CreatePageViewModel createPageViewModel)
		{
            vm = new ListTasksPageViewModel()
            {
                ToDoItems = createPageViewModel.ToDoItems
            };
            BindingContext = this;
			InitializeComponent ();
		}

        public void OnSelected(object o, ItemTappedEventArgs e)
        {
            vm.SaveSelectedItem(e);
            DisplayAlert("Chosen!", vm.SelectedItem.TaskName + " was selected!", "Ok");
        }
	}
}
