namespace ToDoListApp.DAL.Entities;
/// <summary>
/// Represents a to-do task associated with a to-do list.
/// </summary>
public class ToDoTask
{
    /// <summary>
    /// Gets or sets the unique identifier of the to-do task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the to-do list to which this task belongs.
    /// </summary>
    public int ToDoListId { get; set; }

    /// <summary>
    /// Gets or sets the name of the to-do task.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the optional description of the to-do task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the to-do task was created.
    /// </summary>
    public DateTime Created { get; set; }
}