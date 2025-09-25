using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Data;

namespace ToDoList
{
    /// <summary>
    /// Логика взаимодействия для Task.xaml
    /// </summary>
        public partial class Task : UserControl, INotifyPropertyChanged
        {
            public event RoutedEventHandler? Edit;
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
                
            public Task(TaskInfo taskInfo)
            {
                InitializeComponent();
                _taskInfo = taskInfo;
                this.DataContext = this;
            }

            private void OnEditTask(object sender, RoutedEventArgs e)
            {            
                Edit?.Invoke(this, e);         
            }

            private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }        
        }
    }
