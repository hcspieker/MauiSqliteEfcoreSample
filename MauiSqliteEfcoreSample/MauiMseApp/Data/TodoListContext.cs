using MauiMseApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MauiMseApp.Data
{
    public class TodoListContext : DbContext
    {
        public DbSet<EtyTodoList> EtyTodoLists { get; set; }
        public DbSet<EtyTodoListItem> EtyTodoListItems { get; set; }

        public TodoListContext()
        {
            SQLitePCL.Batteries_V2.Init();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbFile = Path.Combine(FileSystem.AppDataDirectory, "todolist.db3");
            optionsBuilder.UseSqlite($"Filename={dbFile}");
        }
    }
}
