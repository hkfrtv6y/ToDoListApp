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
    private readonly AppDbContext _context;
    public MainWindow()
    {
        _context = new AppDbContext();
        InitializeComponent();
        AppDataSeeder.Seed(_context);
        userComboBox.ItemsSource = _context.Users.ToList();
        dgridLists.ItemsSource = _context.Lists.ToList();
    }

    private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (userComboBox.SelectedItem is User selectedUser)
        {
            // Filter and display ToDoLists for the selected user
            dgridLists.ItemsSource = selectedUser.Lists.ToList();
        }
    }
}
