using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Constructor of <c>MainWindow</c> class
    /// </summary>
    public MainWindow()
    {
        using AppDbContext dbContext = new(); // connect to database with 'using' directive to dispose of the object after
        AppDataSeeder.Seed(dbContext); // seed the database if it's empty
        InitializeComponent();
        userComboBox.ItemsSource = dbContext.Users.ToList(); // populate users combobox
        userComboBox.SelectedIndex = 0; // by default a user is selected - users are hardcoded into the database
        dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == ((User)userComboBox.SelectedItem).Id).Include(l => l.Tasks).ToList();
    }

    // reload datagrid after user change
    private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (userComboBox.SelectedItem is User selectedUser)
        {
            using AppDbContext dbContext = new();
            dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == selectedUser.Id).Include(l => l.Tasks).ToList();
        }
    }

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

        // reload data grid
        using AppDbContext dbContext = new();
        dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == user.Id).ToList();
    }

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
    private void DeleteList_Click(object sender, RoutedEventArgs e)
    {
        if (dgridLists.SelectedItem is ToDoList selectedList)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this list and all its tasks?", "Delete list", MessageBoxButton.YesNo);
            using AppDbContext dbContext = new();
            if (result == MessageBoxResult.Yes)
            {

                // Find the list and its associated tasks in the database
                ToDoList? listToDelete = dbContext.Lists.FirstOrDefault(l => l.Id == selectedList.Id);
                if (listToDelete is not null)
                {
                    // Remove the list and its tasks
                    dbContext.Tasks.RemoveRange(listToDelete.Tasks);
                    dbContext.Lists.Remove(listToDelete);
                    dbContext.SaveChanges();
                }
            }
            User? user = userComboBox.SelectedItem as User;
            if (user is null)
            {
                MessageBox.Show("Error: please select user first.");
                return;
            }
            dgridLists.ItemsSource = dbContext.Lists.Where(l => l.UserId == user.Id).ToList();
        }
    }

}
