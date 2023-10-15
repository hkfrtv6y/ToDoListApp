namespace ToDoListApp.DAL.Entities;
/// <summary>
/// Represents an address associated with a user.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the unique identifier of the address.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user ID associated with this address.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    public string Street { get; set; } = default!;

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    public string PostalCode { get; set; } = default!;

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    public string Country { get; set; } = default!;
}
