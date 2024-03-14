# Setup

1. Add a reference the nuget package `Microsoft.EntityFrameworkCore.Sqlite`
2. Create the model classes (e. g. [EtyTodoList](MauiSqliteEfcoreSample/MauiMseApp/Data/Entity/EtyTodoList.cs) and [EtyTodoListItem](MauiSqliteEfcoreSample/MauiMseApp/Data/Entity/EtyTodoListItem.cs))
3. Create the [DbContext](MauiSqliteEfcoreSample/MauiMseApp/Data/TodoListContext.cs)

# CRUD Operations

## Create

```csharp
var entry = new EtyTodoList { Title = "Example List" };
using var context = new TodoListContext();
context.Add(entry);
await context.SaveChangesAsync();
```

After calling `await context.SaveChangesAsync();` the Id field of the `entry` object is filled automatically.

## Read

Selecting data:
```csharp
using var context = new TodoListContext();
var entries = context.EtyTodoLists.ToList();
```

Selecting data including child items:
```csharp
using var context = new TodoListContext();
var entries = context.EtyTodoLists.Include(x => x.EtyTodoListItems).ToList();
```

## Update

```csharp
using var context = new TodoListContext();
var entry = context.EtyTodoListItems.First();
entry.IsChecked = !entry.IsChecked;
await context.SaveChangesAsync();
```

## Delete

```csharp
var entry = context.EtyTodoListItems.First();
context.Remove(entry);
await context.SaveChangesAsync();
```
