using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using ToDo.ViewModels;
using ToDo.Interfaces;
using ToDo.Core.Models;

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
        private IAuthenticator mAuthenticator;
        private IToDoItemDatabase<ToDoItem> mDatabase;

        //standard values
        public const string TaskNameFontSize = "16";
        public const string TaskDetailFontSize = "14";
        public const string TaskNameFontAttributes = "Bold,Italic";
        public const string TaskLabelFontAttributes = "Bold";

        public ListTasksPage(IAuthenticator authenticator, IToDoItemDatabase<ToDoItem> database)
		{
            InitializeComponent();
            WireUpEventHandlers();
            vm = new ListTasksPageViewModel(ToDo.Database);
            BindingContext = this;
            mAuthenticator = authenticator;
            mDatabase = database;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (mAuthenticator.Authenticated)
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
            if (mAuthenticator != null)
            {
                await mDatabase.InitializeAsync();
                
                if (Device.RuntimePlatform == Device.UWP)
                {
                    mAuthenticator.SetClient(mDatabase.MobileService);
                }

                await mAuthenticator.Authenticate();
                SetUiPerAuthenticated();
            }
        }

        public async void OnLogout(object sender, EventArgs e)
        {
            await mDatabase.InitializeAsync();
            await mAuthenticator.Logout();
            SetUiPerAuthenticated();            
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

        private async void SetUiPerAuthenticated()
        {
            if (mAuthenticator.Authenticated)
            {
                SetAuthenticatedUi();
                await RefreshTaskList();
            }
            else
            {
                SetNotAuthenticatedUi();
            }
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
