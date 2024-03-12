using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMseApp.Data;
using MauiMseApp.Data.Entity;
using MauiMseApp.Models;
using Microsoft.EntityFrameworkCore;
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
        void Appearing()
        {
            TodoLists.Clear();

            using var context = new TodoListContext();
            var entries = context.EtyTodoLists.Include(x => x.EtyTodoListItems).Select(x => new TodoList(x)).ToList();
            if (!entries.Any()) return;

            foreach (var entry in entries)
            {
                TodoLists.Add(entry);
            }
        }

        [RelayCommand]
        async Task AddTodoList()
        {
            if (string.IsNullOrWhiteSpace(NewTodoListTitle))
                return;

            var entry = new EtyTodoList { Title = NewTodoListTitle };
            using var context = new TodoListContext();
            context.Add(entry);
            await context.SaveChangesAsync();

            TodoLists.Add(new TodoList(entry));
            NewTodoListTitle = string.Empty;
        }

        [RelayCommand]
        async Task AddTodoListItem(TodoList todoList)
        {
            if (string.IsNullOrWhiteSpace(todoList.ItemToAdd))
                return;

            var entry = new EtyTodoListItem { EtyTodoListId = todoList.Id, Title = todoList.ItemToAdd };
            using var context = new TodoListContext();
            context.Add(entry);
            await context.SaveChangesAsync();

            todoList.Items.Add(new TodoListItem(entry));
            todoList.ItemToAdd = string.Empty;
        }
    }
}
