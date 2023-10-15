using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
    private ObservableCollection<ToDoList> toDoLists;
    public MainWindow()
    {
        using AppDbContext dbContext = new();
        InitializeComponent();
        AppDataSeeder.Seed(dbContext);
        toDoLists = new ObservableCollection<ToDoList>(dbContext.Lists.Include(l => l.Tasks));
        userComboBox.ItemsSource = dbContext.Users.ToList();
        dgridLists.ItemsSource = toDoLists;
    }

    private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (userComboBox.SelectedItem is User selectedUser)
        {
            // Filter and display ToDoLists for the selected user
            dgridLists.ItemsSource = selectedUser.Lists.ToList();
        }
    }

    private void NewListButton_Click(object sender, RoutedEventArgs e)
    {
        if (userComboBox.SelectedItem is null)
        {
            MessageBox.Show("Error: no user was selected.");
            return;
        }
        NewListWindow window = new();
        User user = userComboBox.SelectedItem as User;
        window.txtUser.Text = user.FullName;
        window.UserId = user.Id;
        window.ShowDialog();

        // reload data grid
        using AppDbContext dbContext = new();
        toDoLists = new ObservableCollection<ToDoList>(dbContext.Lists.Where(l => l.UserId == user.Id).ToList());
        dgridLists.ItemsSource = toDoLists;
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
            User user = userComboBox.SelectedItem as User;
            toDoLists = new ObservableCollection<ToDoList>(dbContext.Lists.Where(l => l.UserId == user.Id).ToList());
            dgridLists.ItemsSource = toDoLists;
        }
    }

}
