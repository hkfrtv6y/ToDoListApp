using Microsoft.EntityFrameworkCore;
using ToDoListApp.DAL.Entities;

namespace ToDoListApp.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ToDoList> Lists { get; set; }
    public DbSet<ToDoTask> Tasks { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoListApp;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the relationships between entities

        // Configure the one-to-many relationship between User and Address
        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<Address>(a => a.UserId);

        // Configure the one-to-many relationship between User and ToDoList
        modelBuilder.Entity<User>()
            .HasMany(u => u.Lists)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId);

        // Configure the one-to-many relationship between ToDoList and ToDoTask
        modelBuilder.Entity<ToDoList>()
            .HasMany(l => l.Tasks)
            .WithOne(t => t.List)
            .HasForeignKey(t => t.ToDoListId);

        // Configure the many-to-one relationship between ToDoTask and ToDoList
        modelBuilder.Entity<ToDoTask>()
            .HasOne(t => t.List)
            .WithMany(l => l.Tasks)
            .HasForeignKey(t => t.ToDoListId);

        // Don't forget to call the base OnModelCreating method to apply any other configurations
        base.OnModelCreating(modelBuilder);
    }
}
