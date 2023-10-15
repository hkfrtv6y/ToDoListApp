using System;
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
    private readonly int _listId;
    public ViewTasksWindow(int id)
    {
        _listId = id;
        InitializeComponent();
        using AppDbContext dbContext = new();
        dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
    }
    private void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (dgridTasks.SelectedItem is ToDoTask selectedTask)
        {
            using AppDbContext dbContext = new();
            ToDoTask? taskToDelete = dbContext.Tasks.FirstOrDefault(l => l.Id == selectedTask.Id);
            dbContext.Tasks.Remove(taskToDelete);
            dbContext.SaveChanges();
            //reload datagrid
            dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
        }
    }

    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            MessageBox.Show("Error: new task name must not be empty.");
            return;
        }
        if (txtName.Text.Length > 100)
        {
            MessageBox.Show("Error: new task name can be maximum 100 characters long");
            return;
        }
        if (txtDescription.Text.Length > 500)
        {
            MessageBox.Show("Error: new task description can be maximum 500 characters long");
            return;
        }

        using AppDbContext dbContext = new();
        dbContext.Tasks.Add(new ToDoTask()
        {
            ToDoListId = _listId,
            Name = txtName.Text,
            Description = txtDescription.Text,
            Created = DateTime.Now
        });
        dbContext.SaveChanges();

        //reload datagrid
        dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
