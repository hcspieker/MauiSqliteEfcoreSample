using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMseApp.Models
{
    public partial class TodoListItem : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [ObservableProperty]
        bool isChecked;
    }
}
