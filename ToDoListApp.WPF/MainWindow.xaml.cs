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
        toDoLists = new ObservableCollection<ToDoList>(dbContext.Lists);
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
            MessageBox.Show("Error: no user selected.");
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

    }
    private void DeleteTasks_Click(object sender, RoutedEventArgs e)
    {

    }
}
