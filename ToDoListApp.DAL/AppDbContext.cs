using Microsoft.EntityFrameworkCore;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.DAL;

/// <summary>
/// Represents the application's database context for Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    public AppDbContext()
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of to-do lists in the database.
    /// </summary>
    public DbSet<ToDoList> Lists { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of to-do tasks in the database.
    /// </summary>
    public DbSet<ToDoTask> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of addresses in the database.
    /// </summary>
    public DbSet<Address> Addresses { get; set; }

    /// <summary>
    /// Configures the database connection and options.
    /// </summary>
    /// <param name="options">The options builder for configuring the database connection.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoListApp;Trusted_Connection=True");
    }

    /// <summary>
    /// Configures the model's relationships and constraints.
    /// </summary>
    /// <param name="modelBuilder">The model builder for configuring the relationships between entities.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the relationships between entities

        // Configure the one-to-one relationship between User and Address
        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<Address>(a => a.UserId);

        // Configure the one-to-many relationship between User and ToDoList
        modelBuilder.Entity<User>()
            .HasMany(u => u.Lists);

        // Configure the one-to-many relationship between ToDoList and ToDoTask
        modelBuilder.Entity<ToDoList>()
            .HasMany(l => l.Tasks);

        // Don't forget to call the base OnModelCreating method to apply any other configurations
        base.OnModelCreating(modelBuilder);
    }
}
