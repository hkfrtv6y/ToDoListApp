namespace ToDoListApp.DAL.Entities;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public List<ToDoList> Lists { get; set; } = new List<ToDoList>();
    public Address? Address { get; set; }
    public string FullName => $"{Id}. {FirstName} {LastName}";

    public override string ToString() => FullName;
}
