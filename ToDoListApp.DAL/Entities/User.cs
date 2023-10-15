namespace ToDoListApp.DAL.Entities;

/// <summary>
/// Represents a user in the application.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets a list of to-do lists associated with this user.
    /// </summary>
    public List<ToDoList> Lists { get; set; } = new List<ToDoList>();

    /// <summary>
    /// Gets or sets the user's address.
    /// </summary>
    public Address? Address { get; set; }

    /// <summary>
    /// Gets the full name of the user in the format 'ID. First Name Last Name'.
    /// </summary>
    public string FullName => $"{Id}. {FirstName} {LastName}";

    /// <summary>
    /// Returns the user's full name as a string.
    /// </summary>
    public override string ToString() => FullName;
}
