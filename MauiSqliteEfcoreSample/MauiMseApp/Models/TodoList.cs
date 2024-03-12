using CommunityToolkit.Mvvm.ComponentModel;
using MauiMseApp.Data.Entity;
using System.Collections.ObjectModel;

namespace MauiMseApp.Models
{
    public partial class TodoList : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ObservableCollection<TodoListItem> Items { get; set; } = new();

        [ObservableProperty]
        string itemToAdd = string.Empty;

        public TodoList()
        {
        }

        public TodoList(EtyTodoList etyTodoList)
        {
            Id = etyTodoList.EtyTodoListId;
            Title = etyTodoList.Title;
            Items = new ObservableCollection<TodoListItem>(etyTodoList.EtyTodoListItems
                .Select(x => new TodoListItem(x)));
        }
    }
}
