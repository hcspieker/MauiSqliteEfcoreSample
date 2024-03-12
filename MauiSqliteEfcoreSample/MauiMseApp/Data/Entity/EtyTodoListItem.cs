namespace MauiMseApp.Data.Entity
{
    public class EtyTodoListItem
    {
        public int EtyTodoListItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsChecked { get; set; }

        public int EtyTodoListId { get; set; }
        public EtyTodoList? EtyTodoList { get; set; }
    }
}
