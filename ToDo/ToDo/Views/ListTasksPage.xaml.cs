using Xamarin.Forms;

using ToDo.ViewModels;

namespace ToDo
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
            vm = new ListTasksPageViewModel();
            vm.ToDoItems = createPageViewModel.ToDoItems;
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
