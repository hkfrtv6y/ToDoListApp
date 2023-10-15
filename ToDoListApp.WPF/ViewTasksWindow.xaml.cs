using System;
using System.Linq;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;

/// <summary>
/// Represents the window for viewing and managing tasks in a to-do list.
/// </summary>
public partial class ViewTasksWindow : Window
{
    private readonly int _listId;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewTasksWindow"/> class.
    /// </summary>
    /// <param name="id">The ID of the to-do list for which tasks are displayed.</param>
    public ViewTasksWindow(int id)
    {
        _listId = id;
        InitializeComponent();
        using AppDbContext dbContext = new();
        dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
    }

    /// <summary>
    /// Handles the click event of the "Delete Task" button.
    /// </summary>
    private void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (dgridTasks.SelectedItem is ToDoTask selectedTask)
        {
            using AppDbContext dbContext = new();
            ToDoTask? taskToDelete = dbContext.Tasks.FirstOrDefault(l => l.Id == selectedTask.Id);
            dbContext.Tasks.Remove(taskToDelete);
            dbContext.SaveChanges();
            // Reload data grid
            dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
        }
    }

    /// <summary>
    /// Handles the click event of the "Add Task" button.
    /// </summary>
    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            MessageBox.Show("Error: new task name must not be empty.");
            return;
        }
        if (txtName.Text.Length > 100)
        {
            MessageBox.Show("Error: new task name can be a maximum of 100 characters long.");
            return;
        }
        if (txtDescription.Text.Length > 500)
        {
            MessageBox.Show("Error: new task description can be a maximum of 500 characters long.");
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

        // Reload data grid
        dgridTasks.ItemsSource = dbContext.Tasks.Where(t => t.ToDoListId == _listId).ToList();
    }

    /// <summary>
    /// Handles the click event of the "Close" button.
    /// </summary>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
