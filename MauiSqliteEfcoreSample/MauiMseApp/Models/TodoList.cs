using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiMseApp.Models
{
    public partial class TodoList : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ObservableCollection<TodoListItem> Items { get; set; } = new();

        [ObservableProperty]
        string itemToAdd;
    }
}
