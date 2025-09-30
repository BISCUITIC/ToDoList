using ToDoList.Data;

namespace ToDoList.DataProvider;

public interface IDataProvider
{    
    //void SaveAllData(IEnumerable<TaskInfo> tasks);
    IEnumerable<TaskInfo> GetAllData();
    void Edit(TaskInfo task);
    void Add(TaskInfo task);
    void Delete(TaskInfo task);
}
