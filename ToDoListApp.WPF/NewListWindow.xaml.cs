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
    internal int UserId;
    public NewListWindow()
    {
        InitializeComponent();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            MessageBox.Show("Error: new list name must not be empty.");
            return;
        }
        using AppDbContext dbContext = new();
        dbContext.Lists.Add(new ToDoList()
        {
            Created = DateTime.Now,
            Name = txtName.Text,
            UserId = UserId
        });
        dbContext.SaveChanges();
        Close();
    }
}
