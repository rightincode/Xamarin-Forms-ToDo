using System;
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

        private bool secondLoad;

        public ListTasksPage()
		{
            InitializeComponent();
            vm = new ListTasksPageViewModel();
            BindingContext = this;
            secondLoad = false;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VM.LoadItemsAsync();
            this.ToDoList.ItemsSource = null;
            this.ToDoList.ItemsSource = VM.ToDoItems;
        }

        public async void OnAddNew(object o, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
        }

        public async void OnSelected(object o, ItemTappedEventArgs e)
        {
            VM.SaveSelectedItem(e);
            //DisplayAlert("Chosen!", VM.SelectedItem.TaskName + " was selected!", "Ok");

            await Navigation.PushAsync(new CreatePage(VM.SelectedItem.ID));
        }
	}
}
