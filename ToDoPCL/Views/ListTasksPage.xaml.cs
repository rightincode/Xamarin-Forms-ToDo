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
                SetAuthenticatedUi();
                await RefreshTaskList();
            }
            else
            {
                SetNotAuthenticatedUi();
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
                await ToDoPCL.Database.InitializeAsync();
                
                if (Device.RuntimePlatform == Device.UWP)
                {
                    ToDoPCL.Authenticator.SetClient(ToDoPCL.Database.MobileService);
                }

                authenticated = await ToDoPCL.Authenticator.Authenticate();

                if (authenticated)
                {
                    SetAuthenticatedUi();
                    await RefreshTaskList();
                }
                else
                {
                    SetNotAuthenticatedUi();
                }
            }
        }

        public async void OnLogout(object sender, EventArgs e)
        {
            await ToDoPCL.Database.InitializeAsync();
            authenticated = await ToDoPCL.Authenticator.Logout();

            if (authenticated)
            {
                SetAuthenticatedUi();
                await RefreshTaskList();
            }
            else
            {
                SetNotAuthenticatedUi();
            }
        }

        private void WireUpEventHandlers()
        {
            ToDoList.Refreshing += OnListRefresh;
            ToDoList.ItemTapped += OnTapped;
            addNewItemBtn.Clicked += OnAddNew;
            loginBtn.Clicked += OnLogin;
            logoutBtn.Clicked += OnLogout;

            ToDoList.IsPullToRefreshEnabled = true;
        }

        private async Task RefreshTaskList()
        {
            await VM.LoadItemsAsync(true);
            ToDoList.ItemsSource = null;
            ToDoList.ItemsSource = VM.ToDoItems;
            ToDoList.IsRefreshing = false;
        }

        private void SetAuthenticatedUi()
        {
            loginBtn.IsVisible = false;
            logoutBtn.IsVisible = true;
            addNewItemBtn.IsVisible = true;
            ToDoList.IsVisible = true;
        }

        private void SetNotAuthenticatedUi()
        {
            loginBtn.IsVisible = true;
            logoutBtn.IsVisible = false;
            addNewItemBtn.IsVisible = false;
            ToDoList.IsVisible = false;
        }
	}
}
