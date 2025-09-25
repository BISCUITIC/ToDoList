using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ToDoList.Data;
using ToDoList.DataProvider;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataProvider _dataProvider; 
        private Task? _activeTask;
        private List<TaskInfo> _tasksInfo;

        public MainWindow()
        {
            InitializeComponent();
            _dataProvider = new JsonDataHandler();

            this.Loaded += MainWindow_Loaded;            
            this.Closing += MainWindow_Closing;
        }

        private void OnEditTask(object task, RoutedEventArgs e)
        {
            _activeTask = task as Task;
            if (_activeTask is null) throw new NullReferenceException("Не удалось преобразовать к Task");

            TaskTitlen.Text = _activeTask.Title;
            TaskDescription.Text = _activeTask.Description;
        }

        private void TaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)            
                Save();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void Save()
        {
            if (_activeTask is null) return;

            _activeTask.Title = TaskTitlen.Text;
            _activeTask.Description = TaskDescription.Text;
        }

        private void OnDeleteTask(object sender, RoutedEventArgs e)
        {
            if (_activeTask is null) return;

            _tasksInfo.Remove(_activeTask.TaskInfo);
            TaskList.Children.Remove(_activeTask);
        }

        private void AddNewTask(object sender, RoutedEventArgs e)
        {
            TaskInfo taskInfo = new TaskInfo();
            _tasksInfo.Add(taskInfo);

            AddNewTaskToTaskList(taskInfo);
        }
        
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {           
            Search(SearchTextBox.Text.ToLower());            
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)            
                Search(SearchTextBox.Text.ToLower());           
        }
        private void Search(string request)
        {
            TaskList.Children.Clear();

            if (string.IsNullOrEmpty(request))
            {
                CreateDefualtTaskList();
                return;
            }

            var searchedTasksInfo = _tasksInfo.Where((task) => task.Title.ToLower().Contains(request));
            foreach (var taskInfo in searchedTasksInfo)
            {
                AddNewTaskToTaskList(taskInfo);
            }
        }

        private void AddNewTaskToTaskList(TaskInfo taskInfo)
        {
            Task newTask = new Task(taskInfo);
            newTask.Edit += OnEditTask;

            TaskList.Children.Add(newTask);
        }
        private void CreateDefualtTaskList()
        {
            foreach (var taskInfo in _tasksInfo)
            {
                AddNewTaskToTaskList(taskInfo);
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _tasksInfo = _dataProvider.GetAllData().ToList<TaskInfo>();
            MessageBox.Show(_tasksInfo.Count.ToString());
            foreach (TaskInfo taskInfo in _tasksInfo)
            {
                Task newTask = new Task(taskInfo);
                newTask.Edit += OnEditTask;
                TaskList.Children.Add(newTask);
            }
        }
        
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _dataProvider.SaveAllData(_tasksInfo);
        }
    }
}