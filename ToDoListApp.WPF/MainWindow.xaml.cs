using System.Windows;
using ToDoListApp.DAL;

namespace ToDoListApp.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AppDataSeeder.Seed(new AppDbContext());
    }
}
