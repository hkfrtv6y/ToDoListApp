using System;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;
/// <summary>
/// Interaction logic for NewListWindow.xaml
/// </summary>
public partial class NewListWindow : Window
{
    private readonly int _userId; // passed from constructor
    /// <summary>
    /// Constructor for <c>NewListWindow</c> class
    /// 
    /// </summary>
    /// <param name="id">
    /// Passes user Id from <c>MainWindow</c> class
    /// </param>
    public NewListWindow(int id)
    {
        _userId = id;
        InitializeComponent();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text)) // check if name is not empty
        {
            MessageBox.Show("Error: new list name must not be empty.");
            return;
        }
        if (txtName.Text.Length > 100) // check if name is not too long
        {
            MessageBox.Show("Error: new list name must be maximum 100 characters long.");
            return;
        }
        using AppDbContext dbContext = new(); // 'using' directive in order to dispose of the connection after
        dbContext.Lists.Add(new ToDoList()
        {
            Created = DateTime.Now,
            Name = txtName.Text,
            UserId = _userId
        });
        dbContext.SaveChanges();
        Close();
    }
}
