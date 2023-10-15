using System;
using System.Windows;
using ToDoListApp.DAL;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.WPF;

/// <summary>
/// Represents the window for creating a new to-do list.
/// </summary>
public partial class NewListWindow : Window
{
    private readonly int _userId; // Passed from the constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="NewListWindow"/> class.
    /// </summary>
    /// <param name="id">The ID of the user to whom the new list will belong.</param>
    public NewListWindow(int id)
    {
        _userId = id;
        InitializeComponent();
    }

    /// <summary>
    /// Handles the click event of the "Create" button.
    /// </summary>
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            MessageBox.Show("Error: new list name must not be empty.");
            return;
        }
        if (txtName.Text.Length > 100)
        {
            MessageBox.Show("Error: new list name must be a maximum of 100 characters long.");
            return;
        }
        using AppDbContext dbContext = new();
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
