using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

using ToDo.ViewModels;
using ToDo.Core.Models;

namespace ToDo
{
	public partial class CreatePage : ContentPage
	{
        private string mTodoListItemId;

        public ICommand SaveCommand => new Command(async () => await OnSave());
        public ICommand DeleteCommand => new Command(async () => await OnDelete());
        public ICommand CancelCommand => new Command(async () => await OnCancel());

        public CreatePageViewModel VM { get; }

        public CreatePage()
		{           
            InitializeComponent();
            VM = new CreatePageViewModel(new ToDoItem(), ToDo.Database);
            mTodoListItemId = string.Empty;
            BindingContext = this;
        }

        public CreatePage(string toDoListItemId)
        {
            InitializeComponent();
            VM = new CreatePageViewModel(new ToDoItem(), ToDo.Database);
            mTodoListItemId = toDoListItemId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrEmpty(mTodoListItemId))
            {
                await VM.LoadToDoListItem(mTodoListItemId);
                BindingContext = this;
            }            
        }

        private async Task OnSave()
        {
            await VM.AddToDoItem();
            await Navigation.PopAsync();
        }

        private async Task OnDelete()
        {
            await VM.DeleteToDoItem();
            await Navigation.PopAsync();
        }
        
        private async Task OnCancel()
        {
            await Navigation.PopAsync();
        }

    }
}
