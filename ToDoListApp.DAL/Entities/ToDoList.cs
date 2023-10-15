namespace ToDoListApp.DAL.Entities;

/// <summary>
/// Represents a to-do list associated with a user.
/// </summary>
public class ToDoList
{
    /// <summary>
    /// Gets or sets the unique identifier of the to-do list.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user ID associated with this to-do list.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the to-do list.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the list of to-do tasks associated with this to-do list.
    /// </summary>
    public List<ToDoTask> Tasks { get; set; } = new List<ToDoTask>();

    /// <summary>
    /// Gets or sets the date and time when the to-do list was created.
    /// </summary>
    public DateTime Created { get; set; }
}
