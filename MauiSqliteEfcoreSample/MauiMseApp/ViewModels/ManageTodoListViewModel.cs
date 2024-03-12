using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMseApp.Models;
using System.Collections.ObjectModel;

namespace MauiMseApp.ViewModels
{
    public partial class ManageTodoListViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newTodoListTitle;

        public ObservableCollection<TodoList> TodoLists { get; set; } = new();

        public ManageTodoListViewModel()
        {
            NewTodoListTitle = string.Empty;
        }

        [RelayCommand]
        void AddTodoList()
        {
            if (string.IsNullOrWhiteSpace(NewTodoListTitle))
                return;

            TodoLists.Add(new TodoList { Title = NewTodoListTitle });
            NewTodoListTitle = string.Empty;
        }

        [RelayCommand]
        void AddTodoListItem(TodoList todoList)
        {
            if (string.IsNullOrWhiteSpace(todoList.ItemToAdd))
                return;

            todoList.Items.Add(new TodoListItem { Title = todoList.ItemToAdd });
            todoList.ItemToAdd = string.Empty;
        }
    }
}
