using ToDoList.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace ToDoList.DataProvider;

internal class DbDataHandler : DbContext, IDataProvider
{
    public DbSet<TaskInfo> Tasks { get; set; } = null!;
    public DbDataHandler() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ToDoApp;Trusted_Connection=True;");
    }

    public void Add(TaskInfo task)
    {
        Tasks.Add(task);
        SaveChanges();
    }

    public void Delete(TaskInfo task)
    {
        Tasks.Remove(task);
        SaveChanges();
    }

    public void Edit(TaskInfo task)
    {
        TaskInfo? t = Tasks.Find(task.Id);
        t = task;
        SaveChanges();
    }

    public IEnumerable<TaskInfo> GetAllData()
    {
        return Tasks;
    }
}
