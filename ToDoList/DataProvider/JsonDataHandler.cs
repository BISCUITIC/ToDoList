using System.IO;
using System.Text.Json;
using System.Windows;
using ToDoList.Data;

namespace ToDoList.DataProvider;

public class JsonDataHandler : IDataProvider
{
    private static string _pathToFile = "data.json";
    private List<TaskInfo> _tasksData;

    public JsonDataHandler() 
    {
        GetAllDataFromJson();
    }

    private void GetAllDataFromJson()
    {
        using (FileStream fileStream = new FileStream(_pathToFile, FileMode.OpenOrCreate))
        {
            try
            {
                _tasksData = JsonSerializer.Deserialize<List<TaskInfo>>(fileStream) ?? [];                  
            }
            catch
            {
                _tasksData = [];
            }
        }
    }
    public void SaveAllDataToJson()
    {
        using (FileStream fileStream = new FileStream(_pathToFile, FileMode.Create))
        {            
            JsonSerializer.Serialize<IEnumerable<TaskInfo>>(fileStream, _tasksData);
            MessageBox.Show("Данные сохранены");
        }
    }

    public IEnumerable<TaskInfo> GetAllData()
    {
        return _tasksData;
    }
    public void Edit(TaskInfo task)
    {
        int index = _tasksData.FindIndex((t) => t.Id == task.Id);
        _tasksData[index] = task;
        SaveAllDataToJson();
    }

    public void Add(TaskInfo task)
    {
        _tasksData.Add(task);
        SaveAllDataToJson();
    }

    public void Delete(TaskInfo task)
    {
        _tasksData.Remove(task);
        SaveAllDataToJson();
    }
}
