using System.Linq;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;
/// <summary>
/// Interaction logic for ViewTasks.xaml
/// </summary>
public partial class ViewTasksWindow : Window
{
    public ViewTasksWindow(int id)
    {
        InitializeComponent();
        using AppDbContext dbContext = new();
        dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == id).ToList();
    }
    private void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (dgridTasks.SelectedItem is ToDoTask selectedTask)
        {
            using AppDbContext dbContext = new();
            ToDoTask? taskToDelete = dbContext.Tasks.FirstOrDefault(l => l.Id == selectedTask.Id);
            if (taskToDelete is not null)
            {
                dbContext.Tasks.Remove(taskToDelete);
                dbContext.SaveChanges();
            }
        }
    }

    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {

    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
