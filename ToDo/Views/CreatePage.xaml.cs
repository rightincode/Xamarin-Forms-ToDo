using System;
using Xamarin.Forms;

using ToDo.ViewModels;
using ToDo.Core.Models;

namespace ToDo
{
	public partial class CreatePage : ContentPage
	{
        private string mTodoListItemId;

        public CreatePageViewModel VM { get; }

        public CreatePage()
		{           
            InitializeComponent();
            WireUpEventHandlers();
            VM = new CreatePageViewModel(new ToDoItem(), ToDo.Database);
            mTodoListItemId = string.Empty;
            BindingContext = VM;
        }

        public CreatePage(string toDoListItemId)
        {
            InitializeComponent();
            WireUpEventHandlers();
            VM = new CreatePageViewModel(new ToDoItem(), ToDo.Database);
            mTodoListItemId = toDoListItemId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrEmpty(mTodoListItemId))
            {
                await VM.LoadToDoListItem(mTodoListItemId);
                BindingContext = VM;
            }            
        }

        private async void OnSave(object o, EventArgs e)
        {
            await VM.AddToDoItem();
            await Navigation.PopAsync();
        }

        private async void OnDelete(object o, EventArgs e)
        {
            await VM.DeleteToDoItem();
            await Navigation.PopAsync();
        }
        
        private void OnCancel(object o, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void WireUpEventHandlers()
        {
            saveToDoItemBtn.Clicked += OnSave;
            deleteToDoItemBtn.Clicked += OnDelete;
            cancelBtn.Clicked += OnCancel;
        }
     
    }
}
