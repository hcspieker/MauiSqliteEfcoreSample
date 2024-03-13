using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        async Task Appearing()
        {
            try
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
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        [RelayCommand]
        async Task AddTodoList()
        {
            if (string.IsNullOrWhiteSpace(NewTodoListTitle))
                return;

            try
            {
                var entry = new EtyTodoList { Title = NewTodoListTitle };

                using var context = new TodoListContext();
                context.Add(entry);
                await context.SaveChangesAsync();

                TodoLists.Add(new TodoList(entry));
                NewTodoListTitle = string.Empty;

                await Toast.Make("added list", ToastDuration.Short).Show();
            }
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        [RelayCommand]
        async Task DeleteTodoList(TodoList todoList)
        {
            try
            {
                using var context = new TodoListContext();
                var entry = context.EtyTodoLists.Single(x => x.EtyTodoListId == todoList.Id);

                context.Remove(entry);
                await context.SaveChangesAsync();
                TodoLists.Remove(todoList);

                await Toast.Make("deleted list", ToastDuration.Short).Show();
            }
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        [RelayCommand]
        async Task AddTodoListItem(TodoList todoList)
        {
            if (string.IsNullOrWhiteSpace(todoList.ItemToAdd))
                return;

            try
            {
                var entry = new EtyTodoListItem { EtyTodoListId = todoList.Id, Title = todoList.ItemToAdd };
                using var context = new TodoListContext();
                context.Add(entry);
                await context.SaveChangesAsync();

                todoList.Items.Add(new TodoListItem(entry));
                todoList.ItemToAdd = string.Empty;

                await Toast.Make("added item", ToastDuration.Short).Show();
            }
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        [RelayCommand]
        async Task IsCheckedChanged(TodoListItem item)
        {
            try
            {
                item.IsChecked = !item.IsChecked;

                using var context = new TodoListContext();
                var entry = context.EtyTodoListItems.Single(x => x.EtyTodoListItemId == item.Id);

                entry.IsChecked = item.IsChecked;
                await context.SaveChangesAsync();

                await Toast.Make("saved checkbox state", ToastDuration.Short).Show();
            }
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        [RelayCommand]
        async Task DeleteTodoListItem(TodoListItem item)
        {
            try
            {
                using var context = new TodoListContext();
                var entry = context.EtyTodoListItems.Single(x => x.EtyTodoListItemId == item.Id);
                var parentId = entry.EtyTodoListId;

                context.Remove(entry);
                await context.SaveChangesAsync();

                TodoLists.Single(x => x.Id == parentId)
                    .Items.Remove(item);

                await Toast.Make("deleted list item", ToastDuration.Short).Show();
            }
            catch (Exception e)
            {
                await PrintError(e);
            }
        }

        async Task PrintError(Exception e)
        {
            try
            {
                await Toast.Make($"an error occured: {e}", ToastDuration.Long).Show();
            }
            catch
            {
                // ignored
            }
        }
    }
}
