namespace ToDoListApp.DAL.Entities;
public class ToDoTask
{
    public int Id { get; set; }
    public int ToDoListId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreateTime { get; set; }
}
