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

        //standard values
        public const string TaskNameFontSize = "16";
        public const string TaskDetailFontSize = "14";
        public const string TaskNameFontAttributes = "Bold,Italic";
        public const string TaskLabelFontAttributes = "Bold";

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
            await VM.LoadItemsAsync(true);
            ToDoList.ItemsSource = null;
            ToDoList.ItemsSource = VM.ToDoItems;
        }

        //private async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem == null) //deselection occurred
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        VM.SaveSelectedItem(e);
        //        await Navigation.PushAsync(new CreatePage(VM.SelectedItem.Id));
        //    }
        //}

        private async void OnTapped(object o, ItemTappedEventArgs e)
        {
            VM.SaveSelectedItem(e);
            await Navigation.PushAsync(new CreatePage(VM.SelectedItem.Id));
        }

        private async void OnAddNew(object o, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
        }

        public async void OnListRefresh(object sender, EventArgs e)
        {
            await VM.LoadItemsAsync(true);
            ToDoList.ItemsSource = null;
            ToDoList.ItemsSource = VM.ToDoItems;
            ToDoList.IsRefreshing = false;
        }
        
        private void WireUpEventHandlers()
        {
            ToDoList.Refreshing += OnListRefresh;
            ToDoList.ItemTapped += OnTapped;
            //ToDoList.ItemSelected += OnSelected;
            addNewItemBtn.Clicked += OnAddNew;

            ToDoList.IsPullToRefreshEnabled = true;
        }
	}
}
