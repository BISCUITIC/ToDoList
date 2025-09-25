namespace ToDoList.Data;

public class TaskInfo
{
    private readonly Guid _id;
    private readonly DateTime _time;
    private string _title;
    private string _description;
    private bool _isChecked;
    public Guid Id { get => _id; }
    public DateTime Time { get => _time; }
    public string Title { get => _title; set => _title = value; }
    public string Desciption { get => _description; set => _description = value; }
    public bool IsCheckd { get => _isChecked; set => _isChecked = value; }

    public TaskInfo()
    {
        _id = Guid.NewGuid();
        _time = DateTime.Now;

        _title = "Untilted";
        _description = "";
        _isChecked = false;
    }

    public override string ToString()
    {
        return $"ID: {_id} Title {_title} Time {_time}";
    }
}
