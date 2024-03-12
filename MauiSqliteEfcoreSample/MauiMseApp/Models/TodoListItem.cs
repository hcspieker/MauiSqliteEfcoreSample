using CommunityToolkit.Mvvm.ComponentModel;
using MauiMseApp.Data.Entity;

namespace MauiMseApp.Models
{
    public partial class TodoListItem : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [ObservableProperty]
        bool isChecked;

        public TodoListItem()
        {

        }

        public TodoListItem(EtyTodoListItem etyTodoListItem)
        {
            Id = etyTodoListItem.EtyTodoListItemId;
            Title = etyTodoListItem.Title;
            IsChecked = etyTodoListItem.IsChecked;
        }
    }
}
