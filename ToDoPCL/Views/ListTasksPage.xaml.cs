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

        public ListTasksPage()
		{
            InitializeComponent();
            WireUpEventHandlers();
            vm = new ListTasksPageViewModel();
            BindingContext = this;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VM.LoadItemsAsync();
            ToDoList.ItemsSource = null;
            ToDoList.ItemsSource = VM.ToDoItems;
        }

        private async void OnSelected(object o, ItemTappedEventArgs e)
        {
            VM.SaveSelectedItem(e);
            await Navigation.PushAsync(new CreatePage(VM.SelectedItem.ID));
        }

        private async void OnAddNew(object o, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
        }
        
        private void WireUpEventHandlers()
        {
            addNewItemBtn.Clicked += OnAddNew;
        }
	}
}
