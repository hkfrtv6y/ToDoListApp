namespace ToDoListApp.DAL.Entities;
public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Country { get; set; } = default!;
}
