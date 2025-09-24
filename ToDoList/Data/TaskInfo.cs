namespace ToDoList.Data;

public class TaskInfo
{
    private readonly Guid _id;
    private readonly DateTime _time;
    private string _title;
    private string _description;

    public Guid Id { get => _id; }
    public DateTime Time { get => _time; }
    public string Title { get => _title; set => _title = value; }
    public string Desciption { get => _description; set => _description = value; }


    public TaskInfo()
    {
        _id = Guid.NewGuid();
        _time = DateTime.Now;

        _title = "Untilted";
        _description = "";
    }

    public override string ToString()
    {
        return $"ID: {_id} Title {_title} Time {_time}";
    }
}
