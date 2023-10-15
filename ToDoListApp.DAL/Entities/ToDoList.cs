namespace ToDoListApp.DAL.Entities;
public class ToDoList
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = default!;
    public List<ToDoTask>? Tasks { get; set; }
    public DateTime Created { get; set; }
}