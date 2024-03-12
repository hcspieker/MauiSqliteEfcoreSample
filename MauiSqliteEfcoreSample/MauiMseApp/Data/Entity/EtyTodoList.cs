namespace MauiMseApp.Data.Entity
{
    public class EtyTodoList
    {
        public int EtyTodoListId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<EtyTodoListItem> EtyTodoListItems { get; set; } = new();
    }
}
