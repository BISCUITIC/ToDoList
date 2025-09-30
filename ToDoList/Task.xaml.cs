using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToDoList.Data;

namespace ToDoList;

/// <summary>
/// Логика взаимодействия для Task.xaml
/// </summary>
public partial class Task : UserControl, INotifyPropertyChanged
{
    public event RoutedEventHandler? Edit;
    public event RoutedEventHandler? Change;
    public event RoutedEventHandler? Delete;
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly TaskInfo _taskInfo;
    public TaskInfo TaskInfo => _taskInfo;

    public string Title
    {
        get => _taskInfo.Title;
        set
        {
            if (_taskInfo.Title != value)
            {
                _taskInfo.Title = value;
                OnPropertyChanged("Title");
            }
        }
    }
    public string Description
    {
        get => _taskInfo.Desciption;
        set
        {
            if (_taskInfo.Desciption != value)
            {
                _taskInfo.Desciption = value;
                OnPropertyChanged("Description");
            }
        }
    }
    public bool IsChecked
    {
        get => _taskInfo.IsCheckd;
        set
        {
            if (_taskInfo.IsCheckd != value)
            {
                _taskInfo.IsCheckd = value;
                OnPropertyChanged("IsCheckd");
            }
            ChangeTaskColor();
        }
    }

    public Task(TaskInfo taskInfo)
    {
        InitializeComponent();
        _taskInfo = taskInfo;
        this.DataContext = this;
        ChangeTaskColor();
    }

    private void ChangeTaskColor()
    {
        if (_taskInfo.IsCheckd == true)
        {
            Layout.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(50, 50, 50));
        }
        else
        {
            Layout.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(61, 61, 61));
        }
    }

    private void OnDeleteTask(object sender, RoutedEventArgs e)
    {
        Delete?.Invoke(this, e);
    }

    private void OnEditTask(object sender, RoutedEventArgs e)
    {
        Edit?.Invoke(this, e);
    }
    private void OnChaneTask(object sender, RoutedEventArgs e)
    {
        Change?.Invoke(this, e);
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void IsTaskGotFocus(object sender, RoutedEventArgs e)
    {        
        DeleteButton.Visibility = Visibility.Visible;
    }
    private void IsTaskLostFocus(object sender, RoutedEventArgs e)
    {        
        DeleteButton.Visibility = Visibility.Hidden;
    }
}

