using System;
using System.Threading.Tasks;
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
        private bool authenticated = false;

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

            if (authenticated)
            {
                loginBtn.IsVisible = false;
                addNewItemBtn.IsVisible = true;
                ToDoList.IsVisible = true;

                await RefreshTaskList();
            }
            else
            {
                loginBtn.IsVisible = true;
                addNewItemBtn.IsVisible = false;
                ToDoList.IsVisible = false;
            }            
        }

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
            await RefreshTaskList();
        }
        
        public async void OnLogin(object sender, EventArgs e)
        {
            if (ToDoPCL.Authenticator != null)
            {
                authenticated = await ToDoPCL.Authenticator.Authenticate();

                if (authenticated)
                {
                    await RefreshTaskList();
                }
            }
        }

        private void WireUpEventHandlers()
        {
            ToDoList.Refreshing += OnListRefresh;
            ToDoList.ItemTapped += OnTapped;
            addNewItemBtn.Clicked += OnAddNew;
            loginBtn.Clicked += OnLogin;

            ToDoList.IsPullToRefreshEnabled = true;
        }

        private async Task RefreshTaskList()
        {
            await VM.LoadItemsAsync(true);
            ToDoList.ItemsSource = null;
            ToDoList.ItemsSource = VM.ToDoItems;
            ToDoList.IsRefreshing = false;
        }
	}
}
