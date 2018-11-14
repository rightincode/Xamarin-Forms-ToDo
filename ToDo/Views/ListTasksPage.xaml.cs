using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using ToDo.ViewModels;
using ToDo.Interfaces;
using ToDo.Data.Interfaces;
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

        public ICommand LogInCommand => new Command(async () => await OnLogin());
        public ICommand LogOutCommand => new Command(async () => await OnLogout());
        public ICommand AddNewCommand => new Command(async () => await OnAddNew());
        public ICommand RefreshListCommand => new Command(async () => await RefreshTaskList());

        public ListTasksPage(IAuthenticator authenticator, IToDoItemDatabase<ToDoItem> database)
		{
            InitializeComponent();
            WireUpEventHandlers();
            vm = new ListTasksPageViewModel(database);
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

        public async void OnListRefresh(object sender, EventArgs e)
        {
            await RefreshTaskList();
        }
        
        public async Task OnLogin()
        {
            if (mAuthenticator != null)
            {
                await mDatabase.InitializeAsync();
                
                if (Device.RuntimePlatform == Device.UWP)
                {
                    mAuthenticator.SetClient(mDatabase.MobileService);
                }

                await mAuthenticator.AuthenticateAsync();
                SetUiPerAuthenticated();
            }
        }

        public async Task OnLogout()
        {
            await mDatabase.InitializeAsync();
            await mAuthenticator.LogoutAsync();
            SetUiPerAuthenticated();            
        }

        private async Task OnAddNew()
        {
            await Navigation.PushAsync(new CreatePage());
        }

        private void WireUpEventHandlers()
        {
            ToDoList.Refreshing += OnListRefresh;
            ToDoList.ItemTapped += OnTapped;
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

            if (Device.RuntimePlatform == Device.UWP)
            {
                refreshListBtn.IsVisible = true;
            }
            
            ToDoList.IsVisible = true;
        }

        private void SetNotAuthenticatedUi()
        {
            loginBtn.IsVisible = true;
            logoutBtn.IsVisible = false;
            addNewItemBtn.IsVisible = false;
            refreshListBtn.IsVisible = false;
            ToDoList.IsVisible = false;
        }
	}
}
