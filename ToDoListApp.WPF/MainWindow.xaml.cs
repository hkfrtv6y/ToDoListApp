using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;

/// <summary>
/// Interaction logic for the main window of the application.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Constructor of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        using AppDbContext dbContext = new();
        AppDataSeeder.Seed(dbContext);
        InitializeComponent();
        userComboBox.ItemsSource = dbContext.Users.ToList();
        userComboBox.SelectedIndex = 0;
        dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == ((User)userComboBox.SelectedItem).Id).Include(l => l.Tasks).ToList();
    }

    /// <summary>
    /// Reloads the data grid after a user change.
    /// </summary>
    private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (userComboBox.SelectedItem is User selectedUser)
        {
            using AppDbContext dbContext = new();
            dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == selectedUser.Id).Include(l => l.Tasks).ToList();
        }
    }

    /// <summary>
    /// Handles the click event of the "New List" button.
    /// </summary>
    private void NewListButton_Click(object sender, RoutedEventArgs e)
    {
        if (userComboBox.SelectedItem is null)
        {
            MessageBox.Show("Error: no user was selected.");
            return;
        }
        User? user = (User)userComboBox.SelectedItem;
        NewListWindow window = new(user.Id);
        window.txtUser.Text = user.FullName;
        window.ShowDialog();

        // Reload data grid
        using AppDbContext dbContext = new();
        dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == user.Id).ToList();
    }

    /// <summary>
    /// Handles the click event of the "View Tasks" button.
    /// </summary>
    private void ViewTasks_Click(object sender, RoutedEventArgs e)
    {
        if (dgridLists.SelectedItem is null)
        {
            MessageBox.Show("Error: no list was selected.");
            return;
        }

        int selectedListId = ((ToDoList)dgridLists.SelectedItem).Id;
        ViewTasksWindow window = new(selectedListId);
        window.ShowDialog();
    }

    /// <summary>
    /// Handles the click event of the "Delete List" button.
    /// </summary>
    private void DeleteList_Click(object sender, RoutedEventArgs e)
    {
        if (dgridLists.SelectedItem is ToDoList selectedList)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this list and all its tasks?", "Delete list", MessageBoxButton.YesNo);
            using AppDbContext dbContext = new();
            if (result == MessageBoxResult.Yes)
            {
                ToDoList? listToDelete = dbContext.Lists.FirstOrDefault(l => l.Id == selectedList.Id);
                if (listToDelete is not null)
                {
                    dbContext.Tasks.RemoveRange(listToDelete.Tasks);
                    dbContext.Lists.Remove(listToDelete);
                    dbContext.SaveChanges();
                }
            }
            User? user = userComboBox.SelectedItem as User;
            if (user is null)
            {
                MessageBox.Show("Error: please select a user first.");
                return;
            }
            dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == user.Id).ToList();
        }
    }
}
