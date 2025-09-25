using System.IO;
using System.Text.Json;
using System.Windows;
using ToDoList.Data;

namespace ToDoList.DataProvider;

public class JsonDataHandler : IDataProvider
{
    private static string _pathToFile = "data.json";

    public void ChangeData(TaskInfo task)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskInfo> GetAllData()
    {
        using (FileStream fileStream = new FileStream(_pathToFile, FileMode.OpenOrCreate))
        {
            try
            {
                IEnumerable<TaskInfo> list = JsonSerializer.Deserialize<IEnumerable<TaskInfo>>(fileStream) ?? [];
                return list;
            }
            catch
            {
                return [];
            }
        }
    }

    public void SaveAllData(IEnumerable<TaskInfo> tasks)
    {
        using (FileStream fileStream = new FileStream(_pathToFile, FileMode.Create))
        {            
            JsonSerializer.Serialize<IEnumerable<TaskInfo>>(fileStream, tasks);
            MessageBox.Show("Данные сохранены");
        }
    }
}
