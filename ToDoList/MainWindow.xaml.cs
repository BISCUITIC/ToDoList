using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
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
        private void OnSaveTask(object task, RoutedEventArgs e)
        {
            if (_activeTask is null) return;

            _activeTask.Title = TaskTitlen.Text;
            _activeTask.Description = TaskDescription.Text;
        }

        private void AddNewTask(object sender, RoutedEventArgs e)
        {
            Task newTask = new Task();
            newTask.Edit += OnEditTask;

            TaskList.Children.Add(newTask);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<TaskInfo> tasksInfo = _dataProvider.GetAllData().ToList<TaskInfo>();
            MessageBox.Show(tasksInfo.Count.ToString());
            foreach (TaskInfo taskInfo in tasksInfo)
            {

                Task newTask = new Task(taskInfo);
                newTask.Edit += OnEditTask;
                TaskList.Children.Add(newTask);
            }
        }
        

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<TaskInfo> tasksInfo = new List<TaskInfo>();

            foreach (Task task in TaskList.Children)
            {
                tasksInfo.Add(task.TaskInfo);                
            }

            _dataProvider.SaveAllData(tasksInfo);
        }


    }
}