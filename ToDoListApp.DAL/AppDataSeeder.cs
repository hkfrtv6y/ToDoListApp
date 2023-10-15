using ToDoListApp.DAL.Entities;

namespace ToDoListApp.DAL;

public static class AppDataSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        if (!(dbContext.Users.Any() || dbContext.Lists.Any()))
        {
            List<User> users = new()
        {
            new User()
            {
                FirstName = "John",
                LastName = "Smith",
                Address = new Address()
                {
                    City = "New York",
                    Street = "Wall St. 22/99",
                    PostalCode = "11200",
                    Country = "USA"
                },
                Lists = new List<ToDoList>()
                {
                    new ToDoList()
                    {
                        Name = "friday",
                        Created = DateTime.Now,
                        Tasks = new List<ToDoTask>()
                        {
                            new ToDoTask()
                            {
                                Name = "groceries",
                                Created = DateTime.Now,
                                Description = "milk, bread, eggs"
                            },
                            new ToDoTask()
                            {
                                Name = "study",
                                Created = DateTime.Now,
                                Description = "calculus & biology"
                            },
                            new ToDoTask()
                            {
                                Name = "send some cvs",
                                Created = DateTime.Now
                            },
                        }
                    }
                }
            },
            new User()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Address = new Address()
                {
                    City = "Chicago",
                    Street = "Windy St. 22/99",
                    PostalCode = "23400",
                    Country = "USA"
                },
                Lists = new List<ToDoList>()
                {
                    new ToDoList()
                    {
                        Name = "last october weekend",
                        Created = DateTime.Now,
                        Tasks = new List<ToDoTask>()
                        {
                            new ToDoTask()
                            {
                                Name = "meet susan",
                                Created = DateTime.Now
                            },
                            new ToDoTask()
                            {
                                Name = "clean flat",
                                Created = DateTime.Now,
                                Description = "remember about vacuuming"
                            }
                        }
                    }
                }
            }
        };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}