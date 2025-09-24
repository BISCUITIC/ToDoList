using ToDoList.Data;

namespace ToDoList.DataProvider;

public interface IDataProvider
{
    void SaveAllData(IEnumerable<TaskInfo> tasks);
    IEnumerable<TaskInfo> GetAllData();
    void ChangeData(TaskInfo task);
}
