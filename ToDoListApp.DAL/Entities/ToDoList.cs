namespace ToDoListApp.DAL.Entities;
public class ToDoList
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<ToDoTask> Tasks { get; set; } = new List<ToDoTask>();
    public DateTime Created { get; set; }
}